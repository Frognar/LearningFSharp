module KataTests.PrimeFactors1Tests

open Xunit
open Kata.PrimeFactors1

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorsOf 1)