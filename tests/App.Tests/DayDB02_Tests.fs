module App.Tests.DayDB02_Tests

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
    member _.``insertOrder returns new id, then summary with 0 lines and 0 total``() = task {
        let! id = OrdersDb.insertOrder fx.ConnStr
        Assert.True(id >= 1L)

        let! s = OrdersDb.getOrderSummary fx.ConnStr id
        match s with
        | Some sum ->
            Assert.Equal(id, sum.id)
            Assert.Equal(0, sum.lines)
            Assert.Equal(0m, sum.total)
        | None -> failwith "Expected Some summary"
    }

    [<Fact>]
    member _.``order with two lines returns correct lines count and total``() = task {
        let! orderId = OrdersDb.insertOrder fx.ConnStr

        let! _ = OrdersDb.insertOrderLine fx.ConnStr orderId 42 1 10.00m
        let! _ = OrdersDb.insertOrderLine fx.ConnStr orderId  7 2  5.50m

        let! s = OrdersDb.getOrderSummary fx.ConnStr orderId
        match s with
        | Some sum ->
            Assert.Equal(2,   sum.lines)
            Assert.Equal(21.00m, sum.total)
        | None -> failwith "Expected Some summary"
    }

    [<Fact>]
    member _.``getOrderSummary returns None for missing id``() = task {
        let! missing =
            task {
                let! _ = OrdersDb.insertOrder fx.ConnStr
                return 99999L
            }
        let! s = OrdersDb.getOrderSummary fx.ConnStr missing
        Assert.True(s.IsNone)
    }