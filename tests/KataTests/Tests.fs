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

[<Fact>]
let ``Factors of 5 should be [ 5 ]`` () =
    Assert.StrictEqual([ 5 ], factorOf 5)

[<Fact>]
let ``Factors of 6 should be [ 2; 3 ]`` () =
    Assert.StrictEqual([ 2; 3 ], factorOf 6)

[<Fact>]
let ``Factors of 7 should be [ 7 ]`` () =
    Assert.StrictEqual([ 7 ], factorOf 7)

[<Fact>]
let ``Factors of 8 should be [ 2; 2; 2 ]`` () =
    Assert.StrictEqual([ 2; 2; 2 ], factorOf 8)