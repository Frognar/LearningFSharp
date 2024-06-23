module KataTests.LookAndSay1Tests

open Xunit
open Kata.LookAndSay1

[<Fact>]
let ``"1" -> "11"`` () =
    Assert.Equal("11", lookAndSay "1")

[<Fact>]
let ``"11" -> "21"`` () =
    Assert.Equal("21", lookAndSay "11")

[<Fact>]
let ``"21" -> "1211"`` () =
    Assert.Equal("1211", lookAndSay "21")

[<Fact>]
let ``"1211" -> "111221"`` () =
    Assert.Equal("111221", lookAndSay "1211")

[<Fact>]
let ``"111221" -> "312211"`` () =
    Assert.Equal("312211", lookAndSay "111221")

[<Fact>]
let ``"312211" -> "13112221"`` () =
    Assert.Equal("13112221", lookAndSay "312211")

[<Fact>]
let ``"13112221" -> "1113213211"`` () =
    Assert.Equal("1113213211", lookAndSay "13112221")
    
[<Fact>]
let ``"1" x 0 -> "1"`` () =
    Assert.Equal("1", lookAndSaySequence "1" 0)
    
[<Fact>]
let ``"1" x 1 -> "1:11"`` () =
    Assert.Equal("1:11", lookAndSaySequence "1" 1)
    
[<Fact>]
let ``"1" x 2 -> "1:11:21"`` () =
    Assert.Equal("1:11:21", lookAndSaySequence "1" 2)