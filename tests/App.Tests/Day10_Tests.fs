module App.Tests.Day10_Tests

open Xunit
open App.Domain

let mkPid i =
    match ProductId.create i with Ok v -> v | Error e -> failwithf "bad pid: %s" e
let mkQty i =
    match Quantity.create i with Ok v -> v | Error e -> failwithf "bad qty: %s" e
let mkPrice p =
    match Price.create p with Ok v -> v | Error e -> failwithf "bad price: %s" e
let mkOrder lines =
    match Order.create lines with Ok o -> o | Error e -> failwith e

let l1 = Line.create (mkPid 1) (mkQty 2) (mkPrice 10.00m)
let l2 = Line.create (mkPid 2) (mkQty 1) (mkPrice 5.50m)
let l3 = Line.create (mkPid 3) (mkQty 3) (mkPrice 2.00m)

[<Fact>]
let ``empty has no items`` () =
    let store = OrderStore.inMemory()
    let state0 = store.empty()
    Assert.Empty(store.list state0)

[<Fact>]
let ``add returns new state and OrderId starting from 1`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()
    let orderA = mkOrder [ l1 ]
    let s1, idA = store.add orderA s0

    Assert.Equal(1, OrderId.value idA)
    Assert.Empty(store.list s0)
    let listed = store.list s1
    Assert.Single(listed)
    let (idListed, oListed) = List.head listed
    Assert.Equal(1, OrderId.value idListed)
    Assert.Equal(Order.total orderA, Order.total oListed)

[<Fact>]
let ``add twice increments id and list is sorted by id`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()
    let oa = mkOrder [ l1 ]
    let ob = mkOrder [ l2; l3 ]
    let s1, idA = store.add oa s0
    let s2, idB = store.add ob s1

    Assert.Equal(1, OrderId.value idA)
    Assert.Equal(2, OrderId.value idB)

    let listed = store.list s2
    Assert.Equal(2, listed.Length)
    Assert.Equal(1, listed.[0] |> fst |> OrderId.value)
    Assert.Equal(2, listed.[1] |> fst |> OrderId.value)

[<Fact>]
let ``tryGet returns Some after add and None for unknown id`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()
    let s1, idA = store.add (mkOrder [ l1 ]) s0

    match store.tryGet idA s1 with
    | Some o -> Assert.Equal(20.00m, Order.total o)
    | None -> failwith "expected Some"

    Assert.True(store.tryGet (OrderId.create 2) s1 |> Option.isNone)

[<Fact>]
let ``delete removes existing id and returns false for missing`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()
    let s1, idA = store.add (mkOrder [ l1 ]) s0
    let s2, deleted = store.delete idA s1

    Assert.True(deleted)
    Assert.True(store.tryGet idA s2 |> Option.isNone)
    Assert.Empty(store.list s2)

    let s3, deleted2 = store.delete idA s2
    Assert.False(deleted2)
    Assert.Same(box s2, box s3) |> ignore