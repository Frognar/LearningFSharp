module App.Tests.DayDB01_Tests

open System.Data
open System.Threading.Tasks
open App.Domain
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