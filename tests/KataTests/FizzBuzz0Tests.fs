module KataTests.FizzBuzz0Tests

open Xunit
open Kata.FizzBuzz0

[<Fact>]
let ``Render 1 should be "1"`` () =
    Assert.Equal("1", render 1)

[<Fact>]
let ``Render 2 should be "2"`` () =
    Assert.Equal("2", render 2)

[<Fact>]
let ``Render 3 should be "Fizz"`` () =
    Assert.Equal("Fizz", render 3)

[<Fact>]
let ``Render 4 should be "4"`` () =
    Assert.Equal("4", render 4)

[<Fact>]
let ``Render 5 should be "Buzz"`` () =
    Assert.Equal("Buzz", render 5)

[<Fact>]
let ``Render 6 should be "Fizz"`` () =
    Assert.Equal("Fizz", render 6)

[<Fact>]
let ``Render 9 should be "Fizz"`` () =
    Assert.Equal("Fizz", render 9)

[<Fact>]
let ``Render 10 should be "Buzz"`` () =
    Assert.Equal("Buzz", render 10)

[<Fact>]
let ``Render 15 should be "FizzBuzz"`` () =
    Assert.Equal("FizzBuzz", render 15)

[<Fact>]
let ``fizzBuzz 10 should be [ "1"; "2"; "Fizz"; "4"; "Buzz"; "Fizz"; "7"; "8"; "Fizz"; "Buzz" ]`` () =
    Assert.StrictEqual([ "1"; "2"; "Fizz"; "4"; "Buzz"; "Fizz"; "7"; "8"; "Fizz"; "Buzz" ], fizzBuzz 10)
    
[<Fact>]
let ``fizzBuzz 100`` () =
    Assert.StrictEqual(["1"; "2"; "Fizz"; "4"; "Buzz"; "Fizz"; "7"; "8"; "Fizz"; "Buzz"; "11";
   "Fizz"; "13"; "14"; "FizzBuzz"; "16"; "17"; "Fizz"; "19"; "Buzz"; "Fizz";
   "22"; "23"; "Fizz"; "Buzz"; "26"; "Fizz"; "28"; "29"; "FizzBuzz"; "31";
   "32"; "Fizz"; "34"; "Buzz"; "Fizz"; "37"; "38"; "Fizz"; "Buzz"; "41";
   "Fizz"; "43"; "44"; "FizzBuzz"; "46"; "47"; "Fizz"; "49"; "Buzz"; "Fizz";
   "52"; "53"; "Fizz"; "Buzz"; "56"; "Fizz"; "58"; "59"; "FizzBuzz"; "61";
   "62"; "Fizz"; "64"; "Buzz"; "Fizz"; "67"; "68"; "Fizz"; "Buzz"; "71";
   "Fizz"; "73"; "74"; "FizzBuzz"; "76"; "77"; "Fizz"; "79"; "Buzz"; "Fizz";
   "82"; "83"; "Fizz"; "Buzz"; "86"; "Fizz"; "88"; "89"; "FizzBuzz"; "91";
   "92"; "Fizz"; "94"; "Buzz"; "Fizz"; "97"; "98"; "Fizz"; "Buzz"], fizzBuzz 100)