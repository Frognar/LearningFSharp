module KataTests.LookAndSayTests

open Xunit
open Kata.LookAndSay

[<Theory>]
[<InlineData("1", "11")>]
[<InlineData("11", "21")>]
[<InlineData("21", "1211")>]
let ``Look at X and say Y`` x expected =
    Assert.Equal(expected, lookAndSay x)