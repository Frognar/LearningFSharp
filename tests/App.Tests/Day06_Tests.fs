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
    let l = Line.create (mkPid 7) (mkQty 3) (mkPrice 12.50m)
    Assert.Equal(37.50m, Line.total l)

[<Fact>]
let ``Order.create rejects empty lines`` () =
    match Order.create [] with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "expected Error for empty order"

[<Fact>]
let ``Order.total sums lines`` () =
    let lines =
      [
        Line.create (mkPid 1) (mkQty 2) (mkPrice 10.00m)
        Line.create (mkPid 2) (mkQty 1) (mkPrice 5.50m)
        Line.create (mkPid 3) (mkQty 3) (mkPrice 2.00m)
      ]
    let order =
      match Order.create lines with
      | Ok o -> o
      | Error e -> failwith e
    Assert.Equal(31.50m, Order.total order)