module KataTests.NameInverterTests

open Xunit
open Kata.NameInverter

[<Fact>]
let ``Should be empty when invert empty`` () =
    Assert.Equal("", invert "")

[<Fact>]
let ``Should be firstname when invert firstname`` () =
    Assert.Equal("Horus", invert "Horus")
