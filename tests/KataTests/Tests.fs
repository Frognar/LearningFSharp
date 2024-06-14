module Tests

open Kata.PrimeFactors0
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorOf 1)

[<Fact>]
let ``Factors of 2 should be [ 2 ]`` () =
    Assert.StrictEqual([ 2 ], factorOf 2)

[<Fact>]
let ``Factors of 3 should be [ 3 ]`` () =
    Assert.StrictEqual([ 3 ], factorOf 3)

[<Fact>]
let ``Factors of 4 should be [ 2; 2 ]`` () =
    Assert.StrictEqual([ 2; 2 ], factorOf 4)