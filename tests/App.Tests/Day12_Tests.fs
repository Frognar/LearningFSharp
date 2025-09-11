module App.Tests.Day12_Tests

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
let ``getOrder returns 200 with JSON when found`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()

    let res1, s1 = AppService.createOrder store s0 [ l1 ]
    Assert.Equal(201, res1.Status)

    let id1 =
        match store.list s1 |> List.tryHead with
        | Some (id, _) -> id
        | None -> failwith "expected at least one order"

    let resGet, s2 = AppService.getOrder store s1 id1
    Assert.Same(box s1, box s2) |> ignore

    Assert.Equal(200, resGet.Status)
    Assert.Contains("\"id\":1", resGet.Body)
    Assert.Contains("\"total\":20", resGet.Body)

[<Fact>]
let ``getOrder returns 404 when missing`` () =
    let store = OrderStore.inMemory()
    let s0 = store.empty()

    let _, sX = AppService.createOrder store (store.empty()) [ l2 ]
    let foreignId =
        store.list sX |> List.head |> fst

    let resNotFound, s1 = AppService.getOrder store s0 foreignId
    Assert.Equal(404, resNotFound.Status)
    Assert.Contains("not found", resNotFound.Body, System.StringComparison.OrdinalIgnoreCase)
    Assert.Same(box s0, box s1) |> ignore