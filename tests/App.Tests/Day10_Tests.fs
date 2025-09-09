module App.Tests.Day10_Tests

open Xunit
open App.Domain

[<Fact>]
let ``empty has no items`` () =
    let store = OrderStore.inMemory()
    let state0 = store.empty()
    Assert.Empty(store.list state0)