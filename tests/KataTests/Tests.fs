module Tests

open Kata.PrimeFactors0
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorOf 1)