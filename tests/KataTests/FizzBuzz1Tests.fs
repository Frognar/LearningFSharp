module KataTests.FizzBuzz1Tests

open Kata.FizzBuzz1
open Xunit

[<Fact>]
let ``render 1 should be 1``() =
    Assert.Equal("1", render 1)

[<Fact>]
let ``render 2 should be 2``() =
    Assert.Equal("2", render 2)