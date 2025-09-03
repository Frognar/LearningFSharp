module App.Tests.Day04_Tests

open Xunit
open App.Domain

let squareAsync x = async { return x * x }
let withDelay (millis: int) x =
    async {
        do! Async.Sleep millis
        return x
   }

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

[<Fact>]
let ``asyncMapParallel runs in parallel but keeps input order`` () =
    let input = [ (100,1); (10,2); (50,3); (0,4) ]
    let work (delay: int, x) =
        async {
            do! Async.Sleep delay
            return x * x
        }

    let result =
        async {
            let! r = asyncMapParallel work input
            return r
        }
        |> Async.RunSynchronously
    Assert.Equal<int list>([1;4;9;16], result)
    
[<Fact>]
let ``fetchBoth returns tuple of results`` () =
    let a = withDelay 20 "A"
    let b = withDelay 10 42
    let result = fetchBoth a b |> Async.RunSynchronously
    Assert.Equal(("A", 42), result)