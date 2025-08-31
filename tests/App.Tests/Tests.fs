module Day01_Tests

open Xunit
open App.Domain

[<Fact>]
let ``normalizePhone removes spaces-dashes-dots`` () =
    let input = "123 456-789.00"
    let result = normalizePhone input
    Assert.Equal("12345678900", result)

[<Fact>]
let ``normalizePhone handles brackets and whitespace`` () =
    let input = "  (12) 345-67-89  "
    let result = normalizePhone input
    Assert.Equal("123456789", result)