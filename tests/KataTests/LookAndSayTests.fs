module KataTests.LookAndSayTests

open Xunit
open Kata.LookAndSay

[<Theory>]
[<InlineData("1", "11")>]
[<InlineData("11", "21")>]
[<InlineData("21", "1211")>]
[<InlineData("1211", "111221")>]
[<InlineData("111221", "312211")>]
[<InlineData("312211", "13112221")>]
[<InlineData("13112221", "1113213211")>]
[<InlineData("1113213211", "31131211131221")>]
[<InlineData("31131211131221", "13211311123113112211")>]
let ``Look at X and say Y`` x expected =
    Assert.Equal(expected, lookAndSay x)