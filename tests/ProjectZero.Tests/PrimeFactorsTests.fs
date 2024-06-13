module ProjectZero.Tests.PrimeFactorsTests

open ProjectZero.PrimeFactors
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.Equivalent([], factorsOf 1, true)

[<Fact>]
let ``Factors of 2 should be [ 2 ]`` () =
    Assert.Equivalent([ 2 ], factorsOf 2, true)

[<Fact>]
let ``Factors of 3 should be [ 3 ]`` () =
    Assert.Equivalent([ 3 ], factorsOf 3, true)

[<Fact>]
let ``Factors of 4 should be [ 2; 2 ]`` () =
    Assert.Equivalent([ 2; 2 ], factorsOf 4, true)

[<Fact>]
let ``Factors of 5 should be [ 5 ]`` () =
    Assert.Equivalent([ 5 ], factorsOf 5, true)

[<Fact>]
let ``Factors of 6 should be [ 2; 3 ]`` () =
    Assert.Equivalent([ 2; 3 ], factorsOf 6, true)

[<Fact>]
let ``Factors of 7 should be [ 7 ]`` () =
    Assert.Equivalent([ 7 ], factorsOf 7, true)

[<Fact>]
let ``Factors of 8 should be [ 2; 2; 2 ]`` () =
    Assert.Equivalent([ 2; 2; 2 ], factorsOf 8, true)

[<Fact>]
let ``Factors of 9 should be [ 3; 3 ]`` () =
    Assert.Equivalent([ 3; 3 ], factorsOf 9, true)

[<Fact>]
let ``Factors of 10 should be [ 2; 5 ]`` () =
    Assert.Equivalent([ 2; 5 ], factorsOf 10, true)

[<Fact>]
let ``Factors of 11 should be [ 11 ]`` () =
    Assert.Equivalent([ 11 ], factorsOf 11, true)

[<Fact>]
let ``Factors of 2 * 2 * 3 * 3 * 5 * 5 * 7 * 7 * 11 * 11 should be [ 2; 2; 3; 3; 5; 5; 7; 7; 11; 11 ]`` () =
    Assert.Equivalent([ 2; 2; 3; 3; 5; 5; 7; 7; 11; 11 ], factorsOf (List.reduce (*) [ 2; 2; 3; 3; 5; 5; 7; 7; 11; 11 ]), true)

let intPow bas exp =
    [bas] |> List.replicate exp |> List.concat |> List.reduce (*)

[<Theory>]
[<InlineData(3, 2, 9)>]
[<InlineData(2, 3, 8)>]
[<InlineData(5, 5, 3125)>]
let ``intPow a b should be a^b`` (a: int) b expected =
    Assert.Equal(expected, intPow a b)

[<Fact>]
let ``Factors of 2^31 - 1 should be [ 2^31 - 1 ]`` () =
    Assert.Equivalent([ (intPow 2 31) - 1 ], factorsOf ((intPow 2 31) - 1), true)

[<Fact>]
let ``primesUpTo 1 should be []`` () =
    Assert.Equivalent([], primesUpTo 1, true)
    
[<Fact>]
let ``primesUpTo 2 should be [ 2 ]`` () =
    Assert.Equivalent([ 2 ], primesUpTo 2, true)
    
[<Fact>]
let ``primesUpTo 3 should be [ 2; 3 ]`` () =
    Assert.Equivalent([ 2; 3 ], primesUpTo 3, true)

[<Fact>]
let ``removeMultipliesOfFirst for [ 2; 3; 4; 5; 6; 7; 8; 9 ] should return [ 3; 5; 7; 9 ]`` () =
    Assert.Equivalent([ 3; 5; 7; 9 ], removeMultipliesOfFirst [ 2; 3; 4; 5; 6; 7; 8; 9 ])

[<Fact>]
let ``removeMultipliesOfFirst for [ 3; 5; 7; 9 ] should return [ 5; 7 ]`` () =
    Assert.Equivalent([ 5; 7 ], removeMultipliesOfFirst [ 3; 5; 7; 9 ])