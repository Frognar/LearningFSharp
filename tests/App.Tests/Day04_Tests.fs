module App.Tests.Day04_Tests

open Xunit
open App.Domain


[<Fact>]
let ``asyncBind chains async computations`` () =
    let start = async { return 5 }
    let f x = async { return x * 3 }
    let result = asyncBind f start |> Async.RunSynchronously
    Assert.Equal(15, result)