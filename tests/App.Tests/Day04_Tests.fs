module App.Tests.Day04_Tests

open Xunit
open App.Domain

let squareAsync x = async { return x * x }

[<Fact>]
let ``asyncBind chains async computations`` () =
    let start = async { return 5 }
    let f x = async { return x * 3 }
    let result = asyncBind f start |> Async.RunSynchronously
    Assert.Equal(15, result)

[<Fact>]
let ``asyncMap maps sequentially preserving order`` () =
    let input = [1;2;3;4]
    let result =
        async {
            let! r = asyncMap squareAsync input
            return r
        }
        |> Async.RunSynchronously
    Assert.Equal<int list>([1;4;9;16], result)