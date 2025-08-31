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