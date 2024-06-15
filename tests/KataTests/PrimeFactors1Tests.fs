module KataTests.PrimeFactors1Tests

open Xunit
open Kata.PrimeFactors1

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorsOf 1)

[<Fact>]
let ``Factors of 2 should be [ 2 ]`` () =
    Assert.StrictEqual([ 2 ], factorsOf 2)

[<Fact>]
let ``Factors of 3 should be [ 3 ]`` () =
    Assert.StrictEqual([ 3 ], factorsOf 3)