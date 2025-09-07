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

let l1 = Line.create (mkPid 1) (mkQty 2) (mkPrice 10.00m)
let l2 = Line.create (mkPid 2) (mkQty 1) (mkPrice 5.50m)
let l3 = Line.create (mkPid 3) (mkQty 3) (mkPrice 2.00m)

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
    Assert.Single(items1) |> ignore

    Assert.Equal(1, OrderId.value id1)

    match OrderRepo.tryGet id1 repo1 with
    | Some o -> Assert.Equal(Order.total order, Order.total o)
    | None -> failwith "expected Some"

[<Fact>]
let ``add twice increments id and list is sorted by id`` () =
    let repo0 = OrderRepo.empty()
    let orderA = mkOrder [ l1 ]
    let orderB = mkOrder [ l2; l3 ]

    let repo1, idA = OrderRepo.add orderA repo0
    let repo2, idB = OrderRepo.add orderB repo1

    Assert.Equal(1, OrderId.value idA)
    Assert.Equal(2, OrderId.value idB)

    let listed = OrderRepo.list repo2
    Assert.Equal(2, listed.Length)
    Assert.Equal(1, listed |> List.item 0 |> fst |> OrderId.value)
    Assert.Equal(2, listed |> List.item 1 |> fst |> OrderId.value)

[<Fact>]
let ``tryGet returns None for missing id`` () =
    let repo = OrderRepo.empty()
    let id = OrderId.create 1
    let found = OrderRepo.tryGet id repo
    Assert.True(found.IsNone)