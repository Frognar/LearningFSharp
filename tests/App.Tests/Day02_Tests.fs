module Day02_Tests

open Xunit
open App.Domain

[<Fact>]
let ``map' squares numbers preserving order`` () =
    let input = [1;2;3;4]
    let result = map' (fun x -> x * x) input
    Assert.Equal<int list>([1;4;9;16], result)