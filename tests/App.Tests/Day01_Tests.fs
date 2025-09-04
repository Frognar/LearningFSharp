module App.Tests.Day01_Tests

open Xunit
open App.Domain

[<Fact>]
let ``normalizePhone removes spaces-dashes-dots`` () =
    let result = "123 456-789.00" |> PhoneNumber.create
    Assert.Equal("12345678900", PhoneNumber.value result)

[<Fact>]
let ``normalizePhone handles brackets and whitespace`` () =
    let result = "  (12) 345-67-89  " |> PhoneNumber.create
    Assert.Equal("123456789", PhoneNumber.value result)

let alaEmail = "ala@example.com"

[<Fact>]
let ``tryCreateEmail accepts address with at-sign`` () =
    let result = alaEmail |> EmailAddress.create
    match result with
    | Ok e -> Assert.Equal(alaEmail, EmailAddress.value e)
    | Ok _ | Error _ -> failwith "Expected Ok (Email alaEmail)"

[<Fact>]
let ``tryCreateEmail rejects address without at-sign`` () =
    let result = "alaexample.com" |> EmailAddress.create
    match result with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "Expected Error for invalid email"

[<Fact>]
let ``normalizeName trims leading and trailing whitespace`` () =
    let result = "  Ala  " |> PersonName.create
    Assert.Equal("Ala", PersonName.value result)

[<Fact>]
let ``normalizeName collapses multiple inner spaces`` () =
    let result = "Ala   Nowak" |> PersonName.create
    Assert.Equal("Ala Nowak", PersonName.value result)

let createEmail raw =
    raw
    |> EmailAddress.create
    |> Result.defaultWith (fun err -> failwithf $"Unexpected error: {err}")
    |> Email

[<Fact>]
let ``formatContact formats email contact`` () =
    let email = alaEmail |> createEmail
    let name = "Ala" |> PersonName.create
    let p = (name, email) ||> Person.create

    let text = p |> Person.format

    Assert.Equal("Ala can be contacted via email: ala@example.com", text)

[<Fact>]
let ``formatContact formats phone with normalization`` () =
    let phone = "123 456-789" |> PhoneNumber.create |> Phone
    let name = "Ola" |> PersonName.create
    let p = (name, phone) ||> Person.create
    
    let text = p |> Person.format
    
    Assert.Equal("Ola can be contacted via phone: 123456789", text)

[<Fact>]
let ``withContact returns NEW person and does not mutate original`` () =
    let email = alaEmail |> createEmail
    let name = "Ala" |> PersonName.create
    let original = (name, email) ||> Person.create
    let phone = "111-222-333" |> PhoneNumber.create |> Phone

    let updated = original |> Person.withContact phone

    match Person.contact updated with
    | Phone p -> Assert.Equal("111222333", PhoneNumber.value p)
    | _ -> failwith "Expected Phone after update"

    match Person.contact original with
    | Email e -> Assert.Equal(alaEmail, EmailAddress.value e)
    | _ -> failwith "Original should remain Email"

[<Fact>]
let ``withContact keeps name identity but allows re-normalization in formatContact`` () =
    let email = alaEmail |> createEmail
    let name = "  Ola  Kowalska " |> PersonName.create
    let original = (name, email) ||> Person.create
    let phone = "  (12) 345-67-89  " |> PhoneNumber.create |> Phone

    let updated = original |> Person.withContact phone

    let text = Person.format updated
    Assert.Equal("Ola Kowalska can be contacted via phone: 123456789", text)

[<Fact>]
let ``integration end-to-end formatting`` () =
    let name = "  Ola  Kowalska " |> PersonName.create
    let contact = "  (12) 345-67-89  " |> PhoneNumber.create |> Phone
    let person = (name, contact) ||> Person.create
    let text = person |> Person.format
    Assert.Equal("Ola Kowalska can be contacted via phone: 123456789", text)