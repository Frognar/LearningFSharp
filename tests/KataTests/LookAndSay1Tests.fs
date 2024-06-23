module KataTests.LookAndSay1Tests

open Xunit
open Kata.LookAndSay1

[<Fact>]
let ``"1" -> "11"`` () =
    Assert.Equal("11", lookAndSay "1")