module ProjectZero.Tests.PrimeFactorsTests

open ProjectZero.PrimeFactors
open Xunit

[<Fact>]
let ``Factors of 1 should be []`` () =
    Assert.Equivalent([], factorsOf 1)

