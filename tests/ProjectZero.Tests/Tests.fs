module Tests

open ProjectZero.Calculator
open Xunit

[<Theory>]
[<InlineData(5, 3, 8)>]
[<InlineData(3, 3, 6)>]
[<InlineData(3, 0, 3)>]
let ``Add should correctly sum two numbers`` a b expected =
    Assert.Equal(expected, add a b)
