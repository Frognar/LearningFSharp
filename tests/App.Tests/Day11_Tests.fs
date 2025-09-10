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