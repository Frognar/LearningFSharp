module KataTests.NameInverterTests

open Xunit
open Kata.NameInverter

[<Fact>]
let ``Should be empty when invert empty`` () =
    Assert.Equal("", invert "")