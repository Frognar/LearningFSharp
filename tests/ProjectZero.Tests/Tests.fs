module Tests

open ProjectZero.Calculator
open Xunit

[<Theory>]
[<InlineData(5, 3, 8)>]
let ``Add should correctly sum two numbers`` a b expected =
    Assert.Equal(expected, add a b)
