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