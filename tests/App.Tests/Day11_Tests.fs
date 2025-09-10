module App.Tests.Day11_Tests

open Xunit
open App.Domain

let mkPid i =
    match ProductId.create i with Ok v -> v | Error e -> failwithf "bad pid: %s" e
let mkQty i =
    match Quantity.create i with Ok v -> v | Error e -> failwithf "bad qty: %s" e
let mkPrice p =
    match Price.create p with Ok v -> v | Error e -> failwithf "bad price: %s" e

let l1 = Line.create (mkPid 1) (mkQty 2) (mkPrice 10.00m)
let l2 = Line.create (mkPid 2) (mkQty 1) (mkPrice 5.50m)

[<Fact>]
let ``createOrder returns 201 Created with id and updates state`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()

    let res, s1 = AppService.createOrder store s0 [ l1 ]

    Assert.Equal(201, res.Status)
    Assert.Contains("\"id\":1", res.Body)
    Assert.Contains("\"lines\":1", res.Body)
    Assert.Contains("\"total\":20", res.Body)

    let listed = store.list s1
    Assert.Single(listed)
    Assert.Equal(1, listed.[0] |> fst |> OrderId.value)
    Assert.Equal(20.00m, listed.[0] |> snd |> Order.total)

[<Fact>]
let ``createOrder returns 400 BadRequest and leaves state unchanged on invalid lines`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()

    let res, s1 = AppService.createOrder store s0 []

    Assert.Equal(400, res.Status)
    Assert.Contains("line", res.Body, System.StringComparison.OrdinalIgnoreCase)

    Assert.Same(box s0, box s1) |> ignore
    Assert.Empty(store.list s1)

[<Fact>]
let ``createOrder increments ids for subsequent inserts`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()

    let res1, s1 = AppService.createOrder store s0 [ l1 ]
    let res2, s2 = AppService.createOrder store s1 [ l2 ]

    Assert.Equal(201, res1.Status)
    Assert.Contains("\"id\":1", res1.Body)

    Assert.Equal(201, res2.Status)
    Assert.Contains("\"id\":2", res2.Body)

    let listed = store.list s2
    Assert.Equal(2, listed.Length)
    Assert.Equal(1, listed.[0] |> fst |> OrderId.value)
    Assert.Equal(2, listed.[1] |> fst |> OrderId.value)