module App.Tests.Day08_Tests

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

let l1 = Line.create (mkPid 1) (mkQty 1) (mkPrice 10.00m)

[<Fact>]
let ``empty repo has no items`` () =
    let repo = OrderRepo.empty()
    let items = OrderRepo.list repo
    Assert.Empty(items)

[<Fact>]
let ``add returns NEW repo, original unchanged`` () =
    let repo0 = OrderRepo.empty()
    let order = mkOrder [ l1 ]
    let repo1, id1 = OrderRepo.add order repo0

    Assert.Empty(OrderRepo.list repo0)

    let items1 = OrderRepo.list repo1
    Assert.Single(items1)

    Assert.Equal(1, OrderId.value id1)

    match OrderRepo.tryGet id1 repo1 with
    | Some o -> Assert.Equal(Order.total order, Order.total o)
    | None -> failwith "expected Some"