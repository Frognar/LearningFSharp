module ProjectZero.Tests.PrimeFactorsTests

open ProjectZero.PrimeFactors
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.Equivalent([], factorsOf 1)

[<Fact>]
let ``Factors of 2 should be [ 2 ]`` () =
    Assert.Equivalent([ 2 ], factorsOf 2)

[<Fact>]
let ``Factors of 3 should be [ 3 ]`` () =
    Assert.Equivalent([ 3 ], factorsOf 3)

[<Fact>]
let ``Factors of 4 should be [ 2; 2 ]`` () =
    Assert.Equivalent([ 2; 2 ], factorsOf 4)

[<Fact>]
let ``Factors of 5 should be [ 5 ]`` () =
    Assert.Equivalent([ 5 ], factorsOf 5)

[<Fact>]
let ``Factors of 6 should be [ 2; 3 ]`` () =
    Assert.Equivalent([ 2; 3 ], factorsOf 6)

[<Fact>]
let ``Factors of 7 should be [ 7 ]`` () =
    Assert.Equivalent([ 7 ], factorsOf 7)

[<Fact>]
let ``Factors of 8 should be [ 2; 2; 2 ]`` () =
    Assert.Equivalent([ 2; 2; 2 ], factorsOf 8)

[<Fact>]
let ``Factors of 9 should be [ 3; 3 ]`` () =
    Assert.Equivalent([ 3; 3 ], factorsOf 9)
