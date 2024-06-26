module Tests

open Kata.PrimeFactors0
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.StrictEqual([], factorsOf 1)

[<Fact>]
let ``Factors of 2 should be [ 2 ]`` () =
    Assert.StrictEqual([ 2 ], factorsOf 2)

[<Fact>]
let ``Factors of 3 should be [ 3 ]`` () =
    Assert.StrictEqual([ 3 ], factorsOf 3)

[<Fact>]
let ``Factors of 4 should be [ 2; 2 ]`` () =
    Assert.StrictEqual([ 2; 2 ], factorsOf 4)

[<Fact>]
let ``Factors of 5 should be [ 5 ]`` () =
    Assert.StrictEqual([ 5 ], factorsOf 5)

[<Fact>]
let ``Factors of 6 should be [ 2; 3 ]`` () =
    Assert.StrictEqual([ 2; 3 ], factorsOf 6)

[<Fact>]
let ``Factors of 7 should be [ 7 ]`` () =
    Assert.StrictEqual([ 7 ], factorsOf 7)

[<Fact>]
let ``Factors of 8 should be [ 2; 2; 2 ]`` () =
    Assert.StrictEqual([ 2; 2; 2 ], factorsOf 8)

[<Fact>]
let ``Factors of 9 should be [ 3; 3 ]`` () =
    Assert.StrictEqual([ 3; 3 ], factorsOf 9)

[<Fact>]
let ``Factors of 10 should be [ 2; 5 ]`` () =
    Assert.StrictEqual([ 2; 5 ], factorsOf 10)

[<Fact>]
let ``Factors of 11 should be [ 11 ]`` () =
    Assert.StrictEqual([ 11 ], factorsOf 11)

[<Fact>]
let ``Factors of 12 should be [ 2; 2; 3 ]`` () =
    Assert.StrictEqual([ 2; 2; 3 ], factorsOf 12)

[<Fact>]
let ``Factors of 13 should be [ 13 ]`` () =
    Assert.StrictEqual([ 13 ], factorsOf 13)

[<Fact>]
let ``Factors of 14 should be [ 2; 7 ]`` () =
    Assert.StrictEqual([ 2; 7 ], factorsOf 14)

[<Fact>]
let ``Factors of 15 should be [ 3; 5 ]`` () =
    Assert.StrictEqual([ 3; 5 ], factorsOf 15)

[<Fact>]
let ``Factors of 2 * 2 * 3 * 3 * 5 * 5 * 7 * 7 * 11 * 11 * 13 * 13 should be [ 2; 2; 3; 3; 5; 5; 7; 7; 11; 11; 13; 13 ]`` () =
    Assert.StrictEqual([ 2; 2; 3; 3; 5; 5; 7; 7; 11; 11; 13; 13 ], factorsOf (2 * 2 * 3 * 3 * 5 * 5 * 7 * 7 * 11 * 11 * 13 * 13))