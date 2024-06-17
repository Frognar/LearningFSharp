module KataTests.PrimeFactors2Tests

open Xunit
open Kata.PrimeFactors2

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorsOf 1)

[<Fact>]
let ``Factors of 2 should be [ 2 ]`` () =
    Assert.StrictEqual([ 2 ], factorsOf 2)