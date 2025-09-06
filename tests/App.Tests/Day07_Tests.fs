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

[<Fact>]
let ``orderToJson uses camelCase and contains fields`` () =
    let lines =
      [
        Line.create (mkPid 1) (mkQty 2) (mkPrice 10.00m)
        Line.create (mkPid 2) (mkQty 1) (mkPrice 5.50m)
      ]
    let order =
      match Order.create lines with
      | Ok o -> o
      | Error e -> failwith e

    let json = orderToJson order
    Assert.Contains("\"lines\":2", json)
    Assert.Contains("\"total\":25.5", json)

[<Fact>]
let ``ok returns 200 with body`` () =
    let r = Web.ok """{"msg":"ok"}"""
    Assert.Equal(200, r.Status)
    Assert.Contains("\"ok\"", r.Body)

[<Fact>]
let ``badRequest returns 400 with error string`` () =
    let r = Web.badRequest "Invalid order"
    Assert.Equal(400, r.Status)
    Assert.Contains("Invalid order", r.Body)

[<Fact>]
let ``created returns 201 with body`` () =
    let r = Web.created """{"id":123}"""
    Assert.Equal(201, r.Status)
    Assert.Contains("\"id\":123", r.Body)