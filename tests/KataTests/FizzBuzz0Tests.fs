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