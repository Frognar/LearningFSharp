module App.Tests.Day06_Tests

open Xunit
open App.Domain

[<Theory>]
[<InlineData(1)>]
[<InlineData(42)>]
let ``ProductId.create accepts positives`` (i) =
    match ProductId.create i with
    | Ok _ -> Assert.True(true)
    | Error e -> failwithf "unexpected: %s" e

[<Theory>]
[<InlineData(0)>]
[<InlineData(-5)>]
let ``ProductId.create rejects non-positives`` (i) =
    match ProductId.create i with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "expected Error"

[<Theory>]
[<InlineData(1)>]
[<InlineData(100)>]
let ``Quantity.create accepts in 1..100`` (q) =
    match Quantity.create q with
    | Ok _ -> Assert.True(true)
    | Error e -> failwithf "unexpected: %s" e

[<Theory>]
[<InlineData(0)>]
[<InlineData(101)>]
let ``Quantity.create rejects out of range`` (q) =
    match Quantity.create q with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "expected Error"

[<Theory>]
[<InlineData(0.0)>]
[<InlineData(19.99)>]
let ``Price.create accepts >= 0`` (p: decimal) =
    match Price.create p with
    | Ok _ -> Assert.True(true)
    | Error e -> failwithf "unexpected: %s" e

[<Theory>]
[<InlineData(-0.01)>]
let ``Price.create rejects negative`` (p: decimal) =
    match Price.create p with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "expected Error"

let mkPid i =
    match ProductId.create i with
    | Ok v -> v | Error e -> failwithf $"bad pid: %s{e}"

let mkQty i =
    match Quantity.create i with
    | Ok v -> v | Error e -> failwithf $"bad qty: %s{e}"

let mkPrice p =
    match Price.create p with
    | Ok v -> v | Error e -> failwithf $"bad price: %s{e}"

[<Fact>]
let ``Line.total = qty * unit price`` () =
    let l = { ProductId = mkPid 7; Quantity = mkQty 3; UnitPrice = mkPrice 12.50m }
    Assert.Equal(37.50m, Line.total l)