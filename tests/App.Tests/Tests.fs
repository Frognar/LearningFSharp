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

let alaEmail = "ala@example.com"

[<Fact>]
let ``tryCreateEmail accepts address with at-sign`` () =
    let result = tryCreateEmail alaEmail
    match result with
    | Ok (Email e) -> Assert.Equal(alaEmail, e)
    | Ok _ | Error _ -> failwith "Expected Ok (Email alaEmail)"

[<Fact>]
let ``tryCreateEmail rejects address without at-sign`` () =
    let result = tryCreateEmail "alaexample.com"
    match result with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "Expected Error for invalid email"

[<Fact>]
let ``normalizeName trims leading and trailing whitespace`` () =
    let result = normalizeName "  Ala  "
    Assert.Equal("Ala", result)

[<Fact>]
let ``normalizeName collapses multiple inner spaces`` () =
    let result = normalizeName "Ala   Nowak"
    Assert.Equal("Ala Nowak", result)

[<Fact>]
let ``formatContact formats email contact`` () =
    let p : Person = { Name = "Ala"; Contact = Email alaEmail }
    let text = formatContact p
    Assert.Equal("Ala can be contacted via email: ala@example.com", text)

[<Fact>]
let ``formatContact formats phone with normalization`` () =
    let p : Person = { Name = "Ola"; Contact = Phone "123 456-789" }
    let text = formatContact p
    Assert.Equal("Ola can be contacted via phone: 123456789", text)