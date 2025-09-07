module App.Tests.Day08_Tests

open Xunit
open App.Domain

[<Fact>]
let ``empty repo has no items`` () =
    let repo = OrderRepo.empty()
    let items = OrderRepo.list repo
    Assert.Empty(items)