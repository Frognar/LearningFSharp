module App.Tests.Day03_Tests

open Xunit
open App.Domain


[<Fact>]
let ``rmap maps Ok and leaves Error`` () =
    let ok = Ok 10 |> Rop.rmap (fun x -> x * 2)
    let err = (Error "boom") |> Rop.rmap (fun (_:int) -> 0)
    Assert.Equal(Ok 20, ok)
    Assert.Equal(Error "boom", err)

[<Fact>]
let ``rbind binds Ok and short-circuits Error`` () =
    let parsePositive x =
        if x > 0 then Ok (x * 3) else Error "non-positive"

    let a = Ok 5 |> Rop.rbind parsePositive
    let b = Ok 0 |> Rop.rbind parsePositive
    let c = (Error "nope") |> Rop.rbind parsePositive

    Assert.Equal(Ok 15, a)
    Assert.Equal(Error "non-positive", b)
    Assert.Equal(Error "nope", c)

[<Fact>]
let ``rmapError transforms only error`` () =
    let a = Ok "v" |> Rop.rmapError (fun _ -> "X")
    let b = (Error "bad") |> Rop.rmapError (fun e -> $"ERR:{e}")
    Assert.Equal(Ok "v", a)
    Assert.Equal(Error "ERR:bad", b)

[<Theory>]
[<InlineData("  Ala  ", "Ala")>]
[<InlineData("Jan", "Jan")>]
let ``validateNonEmpty trims and returns original string`` (raw, expected) =
    match validateNonEmpty raw with
    | Ok s -> Assert.Equal(expected, s)
    | Error e -> failwithf "Unexpected error: %s" e

[<Theory>]
[<InlineData("   ")>]
[<InlineData("")>]
let ``validateNonEmpty rejects empty or whitespace`` raw =
    match validateNonEmpty raw with
    | Error "Required" -> Assert.True(true)
    | _ -> failwith "Expected Error \"Required\""

[<Fact>]
let ``registerUser success builds User with normalized name`` () =
    let res = registerUser "  Ola  Kowalska " "ola@example.com"
    match res with
    | Ok u ->
        Assert.Equal("Ola Kowalska", PersonName.value (Person.name u))
        Assert.Equal("ola@example.com", Contact.value (Person.contact u))
    | Error e -> failwithf "Unexpected error: %s" e

[<Fact>]
let ``registerUser fails on empty name`` () =
    let res = registerUser "   " "ala@example.com"
    match res with
    | Error msg -> Assert.Equal("Required", msg)
    | Ok _ -> failwith "Expected Error for empty name"

[<Fact>]
let ``registerUser fails on invalid email`` () =
    let res = registerUser "Ala" "alaexample.com"
    match res with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "Expected Error for bad email"