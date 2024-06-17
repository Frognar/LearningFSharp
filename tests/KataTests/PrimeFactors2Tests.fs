module KataTests.PrimeFactors2Tests

open Xunit
open Kata.PrimeFactors2

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorsOf 1)