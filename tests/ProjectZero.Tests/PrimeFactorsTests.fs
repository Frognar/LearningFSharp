module ProjectZero.Tests.PrimeFactorsTests

open ProjectZero.PrimeFactors
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.Equivalent([], factorsOf 1)

[<Fact>]
let ``Factors of 2 should be [2]`` () =
    Assert.Equivalent([2], factorsOf 2)

[<Fact>]
let ``Factors of 3 should be [3]`` () =
    Assert.Equivalent([3], factorsOf 3)

