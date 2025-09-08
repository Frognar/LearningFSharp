module App.Tests.Day09_Tests

open Xunit
open App.Domain

let mkPid i =
    match ProductId.create i with Ok v -> v | Error e -> failwithf "bad pid: %s" e
let mkQty i =
    match Quantity.create i with Ok v -> v | Error e -> failwithf "bad qty: %s" e
let mkPrice p =
    match Price.create p with Ok v -> v | Error e -> failwithf "bad price: %s" e

let mkOrder lines =
    match Order.create lines with
    | Ok o -> o
    | Error e -> failwith e

[<Fact>]
let ``fromError maps Validation to 400`` () =
    let r = Web.fromError (Validation "Bad data")
    Assert.Equal(400, r.Status)
    Assert.Contains("Bad data", r.Body)

[<Fact>]
let ``fromError maps NotFound to 404`` () =
    let r = Web.fromError (NotFound "Order 123")
    Assert.Equal(404, r.Status)
    Assert.Contains("Order 123", r.Body)

[<Fact>]
let ``fromError maps Conflict to 409`` () =
    let r = Web.fromError (Conflict "Duplicate")
    Assert.Equal(409, r.Status)
    Assert.Contains("Duplicate", r.Body)

[<Fact>]
let ``fromError maps Unauthorized to 401`` () =
    let r = Web.fromError (Unauthorized "No token")
    Assert.Equal(401, r.Status)
    Assert.Contains("No token", r.Body)

[<Fact>]
let ``handleResultE Ok -> 200 + json body`` () =
    let order = mkOrder [ Line.create (mkPid 1) (mkQty 1) (mkPrice 10.00m) ]
    let res = handleResultE (Ok order) orderToJson
    Assert.Equal(200, res.Status)
    Assert.Contains("\"total\":10", res.Body)

[<Fact>]
let ``handleResultE Error -> mapped status + message`` () =
    let res = handleResultE (Error (NotFound "Order not found")) orderToJson
    Assert.Equal(404, res.Status)
    Assert.Contains("Order not found", res.Body)

[<Fact>]
let ``createOrderEndpointE returns 201 on success`` () =
    let createOk lines = lines |> Order.create |> Result.mapError Validation
    let res = createOrderEndpointE createOk [ Line.create (mkPid 1) (mkQty 2) (mkPrice 10.00m) ]
    Assert.Equal(201, res.Status)
    Assert.Contains("\"total\":20", res.Body)

[<Fact>]
let ``createOrderEndpointE returns 400 on validation error`` () =
    let createBad _ = Error (Validation "Order must have at least one line")
    let res = createOrderEndpointE createBad []
    Assert.Equal(400, res.Status)
    Assert.Contains("at least one line", res.Body)