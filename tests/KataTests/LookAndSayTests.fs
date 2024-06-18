module KataTests.LookAndSayTests

open Xunit
open Kata.LookAndSay

[<Theory>]
[<InlineData("1", "11")>]
[<InlineData("11", "21")>]
let ``Look at X and say Y`` x expected =
    Assert.Equal(expected, lookAndSay x)