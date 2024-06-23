module KataTests.NameInverterTests

open Xunit
open Kata.NameInverter

[<Fact>]
let ``Should be empty when invert empty`` () =
    Assert.Equal("", invert "")

[<Fact>]
let ``Should be firstname when invert firstname`` () =
    Assert.Equal("Horus", invert "Horus")

[<Fact>]
let ``Should be 'last, first' when invert 'first last'`` () =
    Assert.Equal("Lupercal, Horus", invert "Horus Lupercal")

[<Fact>]
let ``Should be 'last, first' when invert 'first    last'`` () =
    Assert.Equal("Lupercal, Horus", invert "Horus    Lupercal")

[<Fact>]
let ``Should be 'last, first' when invert 'honorific first last'`` () =
    Assert.Equal("Lupercal, Horus", invert "Mr. Horus Lupercal")

[<Theory>]
[<InlineData("Horus Lupercal Sr.", "Lupercal, Horus Sr.")>]
[<InlineData("Horus Lupercal PhD.", "Lupercal, Horus PhD.")>]
let ``Should be 'last, first postnominal' when invert 'first last postnominal'`` value expected =
    Assert.Equal(expected, invert value)

[<Fact>]
let ``Should be 'last, first postnominals' when invert 'first last postnominals'`` () =
    Assert.Equal("Lupercal, Horus Sr. PhD.", invert "Horus Lupercal Sr. PhD.")
[<Fact>]
let ``Should be 'last, first postnominals' when invert '  honorific   first   last   postnominals   '`` () =
    Assert.Equal("Lupercal, Horus Sr. PhD.", invert "   Mr.   Horus   Lupercal   Sr.   PhD.   ")
