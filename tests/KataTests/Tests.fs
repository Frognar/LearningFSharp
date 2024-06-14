module Tests

open Kata.PrimeFactors0
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorOf 1)

[<Fact>]
let ``Factors of 2 should be [ 2 ]`` () =
    Assert.StrictEqual([ 2 ], factorOf 2)