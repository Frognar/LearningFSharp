module App.Tests.DayDB01_Tests

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