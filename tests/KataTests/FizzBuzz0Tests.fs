module KataTests.FizzBuzz0Tests

open Xunit
open Kata.FizzBuzz0

[<Fact>]
let ``Render 1 should be "1"`` () =
    Assert.Equal("1", render 1)

[<Fact>]
let ``Render 2 should be "2"`` () =
    Assert.Equal("2", render 2)