module App.Tests.DayDB01_Tests

open System
open System.Data
open System.Threading.Tasks
open App.Domain
open Dapper
open Npgsql
open Testcontainers.PostgreSql
open Xunit

type PgFixture() =
    let container =
        PostgreSqlBuilder()
            .WithImage("postgres:16-alpine")
            .WithDatabase("testdb")
            .WithUsername("test")
            .WithPassword("test")
            .Build()

    member _.ConnStr = container.GetConnectionString()

    interface IAsyncLifetime with
      member _.InitializeAsync() =
        task {
          do! container.StartAsync()
          do! Migrations.run (container.GetConnectionString())
        } :> Task

      member _.DisposeAsync() =
        task {
          do! container.DisposeAsync().AsTask()
        } :> Task
        
type DbTests(fx: PgFixture) =
    interface IClassFixture<PgFixture>

    member private _.openConn() =
        let c = new NpgsqlConnection(fx.ConnStr)
        c.Open()
        c :> IDbConnection

    [<Fact>]
    member this.``migrations create required tables``() = task {
        use conn = this.openConn()
        let! tables =
            conn.QueryAsync<string>(
                """
                SELECT table_name
                FROM information_schema.tables
                WHERE table_schema = 'public'
                  AND table_name IN ('orders','order_lines')
                ORDER BY table_name;
                """
            ) |> Async.AwaitTask
        Assert.Equal<string list>(["order_lines"; "orders"], tables |> Seq.toList |> List.sort)
    }

    [<Fact>]
    member this.``orders.id is bigint PK and created_at has default``() = task {
        use conn = this.openConn()
        let! id =
            conn.ExecuteScalarAsync<int64>(
                "INSERT INTO orders DEFAULT VALUES RETURNING id;"
            ) |> Async.AwaitTask

        Assert.True(id >= 1L)

        let! createdAt =
            conn.ExecuteScalarAsync<DateTime>(
                "SELECT created_at FROM orders WHERE id = @id;", dict [ "id", box id ]
            ) |> Async.AwaitTask

        Assert.True((DateTime.UtcNow - createdAt.ToUniversalTime()) < TimeSpan.FromMinutes(5.0))
    }
    
    [<Fact>]
    member this.``order_lines FK to orders and basic checks``() = task {
        use conn = this.openConn()

        let! orderId =
            conn.ExecuteScalarAsync<int64>("INSERT INTO orders DEFAULT VALUES RETURNING id;")
            |> Async.AwaitTask

        let! lineId =
            conn.ExecuteScalarAsync<int64>(
                """
                INSERT INTO order_lines(order_id, product_id, quantity, unit_price)
                VALUES (@order_id, @product_id, @quantity, @unit_price)
                RETURNING id;
                """,
                dict [
                    "order_id", box orderId
                    "product_id", box 42
                    "quantity", box 3
                    "unit_price", box 12.50m
                ]
            ) |> Async.AwaitTask

        Assert.True(lineId >= 1L)

        let! ex1 =
            task {
                try
                    let! _ =
                        conn.ExecuteScalarAsync<int64>(
                            """
                            INSERT INTO order_lines(order_id, product_id, quantity, unit_price)
                            VALUES (@order_id, @product_id, @quantity, @unit_price)
                            RETURNING id;
                            """,
                            dict [
                                "order_id", box orderId
                                "product_id", box 99
                                "quantity", box 0   // niepoprawne
                                "unit_price", box 1.0m
                            ]
                        )
                    return Unchecked.defaultof<exn>
                with e -> return e
            }
        Assert.NotNull(ex1)

        let! ex2 =
            task {
                try
                    let! _ =
                        conn.ExecuteScalarAsync<int64>(
                            """
                            INSERT INTO order_lines(order_id, product_id, quantity, unit_price)
                            VALUES (@order_id, @product_id, @quantity, @unit_price)
                            RETURNING id;
                            """,
                            dict [
                                "order_id", box 999999L
                                "product_id", box 1
                                "quantity", box 1
                                "unit_price", box 1.0m
                            ]
                        )
                    return Unchecked.defaultof<exn>
                with e -> return e
            }
        Assert.NotNull(ex2)
    }

    [<Fact>]
    member this.``ON DELETE CASCADE removes lines with order``() = task {
        use conn = this.openConn()

        let! orderId =
            conn.ExecuteScalarAsync<int64>("INSERT INTO orders DEFAULT VALUES RETURNING id;")
            |> Async.AwaitTask

        do!
            conn.ExecuteAsync(
                """
                INSERT INTO order_lines(order_id, product_id, quantity, unit_price)
                VALUES
                  (@order_id, 1, 1, 10.00),
                  (@order_id, 2, 2,  5.50);
                """,
                dict [ "order_id", box orderId ]
            ) |> Async.AwaitTask |> Async.Ignore

        do!
            conn.ExecuteAsync("DELETE FROM orders WHERE id=@id;", dict [ "id", box orderId ])
            |> Async.AwaitTask |> Async.Ignore

        let! cnt =
            conn.ExecuteScalarAsync<int>(
                "SELECT COUNT(*) FROM order_lines WHERE order_id = @id;",
                dict [ "id", box orderId ]
            ) |> Async.AwaitTask

        Assert.Equal(0, cnt)
    }