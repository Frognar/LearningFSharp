module App.Tests.Day07_Tests

open Xunit
open App.Domain

let mkPid i =
    match ProductId.create i with
    | Ok v -> v | Error e -> failwithf "bad pid: %s" e

let mkQty i =
    match Quantity.create i with
    | Ok v -> v | Error e -> failwithf "bad qty: %s" e

let mkPrice p =
    match Price.create p with
    | Ok v -> v | Error e -> failwithf "bad price: %s" e

[<Fact>]
let ``orderDto exposes lines count and total`` () =
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

    let dto = orderDto order
    Assert.Equal(3, dto.lines)
    Assert.Equal(31.50m, dto.total)