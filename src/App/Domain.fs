namespace App

open System
open System.Text.Json
open System.Text.RegularExpressions

module Domain =
    type PersonName = private PersonName of string

    module PersonName =
        let create raw =
            Regex.Replace(raw, " +", " ").Trim() |> PersonName

        let value (PersonName v) = v

    type EmailAddress = private EmailAddress of string

    module EmailAddress =
        let private rx = Regex(@"^\S+@\S+\.\S+$", RegexOptions.Compiled)

        let create (raw: string) =
            let v = raw.Trim()

            if rx.IsMatch(v) then
                Ok(EmailAddress v)
            else
                Error "Invalid email"

        let value (EmailAddress v) = v

    type PhoneNumber = private PhoneNumber of string

    module PhoneNumber =
        let create raw =
            raw |> String.filter System.Char.IsDigit |> PhoneNumber

        let value (PhoneNumber v) = v


    type Contact =
        | Email of EmailAddress
        | Phone of PhoneNumber
    
    module Contact =
        let value (c: Contact) = match c with
                                    | Email e -> EmailAddress.value e
                                    | Phone p -> PhoneNumber.value p

    type Person =
        private
            { Name: PersonName
              Contact: Contact }

    module Person =
        let create name contact = { Name = name; Contact = contact }

        let withContact contact p = { p with Contact = contact }

        let format p =
            let name: string = PersonName.value p.Name

            match p.Contact with
            | Email e -> $"{name} can be contacted via email: {EmailAddress.value e}"
            | Phone p -> $"{name} can be contacted via phone: {PhoneNumber.value p}"

        let contact (p: Person) = p.Contact
        
        let name (p: Person) = p.Name

    let map' f xs =
        (xs, []) ||> List.foldBack (fun v acc -> f v :: acc)

    let filter' pred xs =
        (xs, []) ||> List.foldBack (fun v acc -> if pred v then v :: acc else acc)

    let reduceOption f xs =
        match xs with
        | [] -> None
        | _ -> Some(xs |> List.reduce f)

    let average xs =
        match xs with
        | [] -> None
        | _ -> Some((List.sum xs) / (float (List.length xs)))

    let median xs =
        match xs with
        | [] -> None
        | _ ->
            let count = xs |> List.length
            let sorted = xs |> List.sort

            if count % 2 = 1 then
                sorted |> List.item (count / 2) |> Some
            else
                let i = count / 2
                let a = sorted |> List.item (i - 1)
                let b = sorted |> List.item i
                Some((a + b) / 2.)

    let variancePop (xs: float list) =
        match xs with
        | [] -> None
        | _ ->
            let avg = xs |> List.average
            let diff = xs |> List.map (fun x -> x - avg)
            let squares = diff |> List.map (fun x -> x * x)
            squares |> List.average |> Some

    let countBy (getKey: 'a -> 'key) (xs: 'a list) : Map<'key, int> =
        (Map.empty, xs)
        ||> List.fold (fun acc v ->
            acc
            |> Map.change (getKey v) (function
                | Some x -> Some(x + 1)
                | None -> Some 1))

    let distinctBy getKey xs =
        let step (seen, acc) v =
            let key = getKey v
            if Set.contains key seen then (seen, acc)
            else (Set.add key seen, v :: acc)

        xs |> List.fold step (Set.empty, []) |> snd |> List.rev

    let clean (input: string) =
        Regex.Replace(input, @"[^a-zA-Z0-9ąćęłńóśźżĄĆĘŁŃÓŚŹŻ\s]", "")

    let splitWords text =
        Regex.Split(text, @"\s+")
        |> Array.filter (fun s -> s <> "")

    let wordCount text =
        text
        |> clean
        |> splitWords
        |> Array.map _.ToLower()
        |> Array.toList
        |> countBy id

    let topNWords map n =
        map
        |> Map.toList
        |> List.sortByDescending snd
        |> List.truncate n
    
    let topWordsFromText n text =
        let map = text |> wordCount
        topNWords map n

    module Rop =
        let rmap f r =
            match r with
            | Ok v -> Ok (f v)
            | Error e -> Error e
        
        let rbind f r =
            match r with
            | Ok v -> f v
            | Error e -> Error e
        
        let rmapError f r =
            match r with
            | Ok v -> Ok v
            | Error e -> Error (f e)

    let validateNonEmpty raw =
        if String.IsNullOrWhiteSpace(raw) then Error "Required"
        else Ok (raw.Trim())

    let registerUser userName userEmail =
        match validateNonEmpty userName with
        | Ok n ->
            let name = PersonName.create n
            let email = EmailAddress.create userEmail
            match email with
            | Ok e -> Ok (Person.create name (Email e))
            | Error e -> Error e
        | Error e -> Error e
    
    let asyncBind f start =
        async {
            let! x = start
            return! f x
        }
    
    let asyncMap f xs =
        let rec loop acc = function
            | [] -> async { return List.rev acc }
            | x :: xt -> async {
                let! y = f x
                return! loop (y :: acc) xt
            }
        
        loop [] xs

    let asyncMapParallel f xs =
        async {
            let! arr = xs |> List.map f |> Async.Parallel
            return Array.toList arr
        }

    let fetchBoth a b =
        async {
            let! a' = Async.StartChild a
            let! b' = Async.StartChild b
            let! ar = a'
            let! br = b'
            return (ar, br)
        }
    
    module Res =
        let map2 f a b =
            match a, b with
            | Ok x, Ok y -> Ok (f x y)
            | Error x, _ -> Error x
            | _, Error y -> Error y

        let apply f a = map2 id f a
        
        let sequence xs =
            let folder acc x =
                match acc, x with
                | Ok acc', Ok x' -> Ok (x' :: acc')
                | Error e, _ -> Error e
                | _, Error e -> Error e

            (Ok [], xs)
            ||> List.fold folder
            |> Rop.rmap List.rev
        
        let traverse f xs =
            xs |> map' f |> sequence
    
    let parseInt (candidate: string) =
        match Int32.TryParse candidate with
        | true, value -> Ok value
        | false, _ -> Error "Not an int"
    
    let sumInts candidates =
        candidates
        |> Res.traverse parseInt
        |> Rop.rmap List.sum
        
    type ProductId = private ProductId of int
    module ProductId =
        let create id =
            if id > 0 then Ok (ProductId id)
            else Error "ProductId must be > 0"

        let value (ProductId i) = i
    
    type Quantity = private Quantity of int
    module Quantity =
        let create quantity=
            if quantity > 0 && quantity < 101 then Ok (Quantity quantity)
            else Error "Quantity must be between 1 and 100"

        let value (Quantity q) = q
    
    type Price = private Price of decimal
    module Price =
        let create price=
            if price >= 0m then Ok (Price price)
            else Error "Price must be >= 0.0"

        let value (Price p) = p

    type Line = private { ProductId: ProductId; Quantity: Quantity; UnitPrice: Price }
    module Line =
        let create id quantity price =
            { ProductId = id; Quantity = quantity; UnitPrice = price }

        let total (v: Line) =
            (decimal (Quantity.value v.Quantity)) * (Price.value v.UnitPrice)

    type Order = private { Lines: Line list }
    module Order =
        let create items =
            match items with
            | [] -> Error "Order must have at least one line"
            | i -> Ok { Lines = i }

        let total order =
            order.Lines |> List.sumBy Line.total
    
    let orderDto order =
        {| lines = order.Lines.Length; total = Order.total order |}

    type DomainError =
        | Validation of string
        | NotFound of string
        | Conflict of string
        | Unauthorized of string

    module Web =
        type Response = { Status: int; Body: string }
        let private jsonOptions =
            let o = JsonSerializerOptions()
            o.PropertyNamingPolicy <- JsonNamingPolicy.CamelCase
            o
    
        let json x = JsonSerializer.Serialize(x, jsonOptions)

        let ok json =
            { Status = 200; Body = json }

        let badRequest msg =
            { Status = 400; Body = msg }
            
        let unauthorized msg =
            { Status = 401; Body = msg }
            
        let notFound msg =
            { Status = 404; Body = msg }
            
        let conflict msg =
            { Status = 409; Body = msg }

        let created json =
            { Status = 201; Body = json }

        let handleResult orderRes toJson =
            match orderRes with
            | Ok order -> ok (order |> toJson)
            | Error error -> badRequest error
        
        let fromError error =
            match error with
            | Validation v -> badRequest v
            | NotFound nf -> notFound nf
            | Conflict c -> conflict c
            | Unauthorized u -> unauthorized u

    let handleResultE orderRes toJson =
        match orderRes with
        | Ok order -> Web.ok (order |> toJson)
        | Error error -> Web.fromError error

    let orderToJson order =
        order |> orderDto |> Web.json

    let createOrderEndpoint createOrder =
        fun lines ->
            match createOrder lines with
            | Ok order -> Web.created (order |> orderToJson)
            | Error error -> Web.badRequest error

    let createOrderEndpointE createOrder =
        fun lines ->
            match createOrder lines with
            | Ok order -> Web.created (order |> orderToJson)
            | Error error -> Web.fromError error
    
    type OrderId = private OrderId of int
    module OrderId =
        let create id =
            OrderId id

        let value (OrderId i) = i

    type OrderRepo = private { nextId: int; items: Map<OrderId, Order> }
    module OrderRepo =
        let empty () =
            { nextId = 1; items = Map.empty }
        
        let list repo =
            repo.items |> Map.toList
        
        let add order repo =
            let idRes = OrderId.create repo.nextId
            let items = repo.items |> Map.add idRes order
            let nextId = repo.nextId + 1
            ({ nextId = nextId; items = items }, idRes)

        let tryGet id repo =
            Map.tryFind id repo.items

        let delete id repo =
            let existed = Map.containsKey id repo.items
            if existed then { repo with items = (Map.remove id repo.items) }, existed
            else repo, false

    type OrderStore<'S> = {
        empty : unit -> 'S
        add: Order -> 'S -> 'S * OrderId
        tryGet: OrderId -> 'S -> Order option
        delete: OrderId -> 'S -> 'S * bool
        list : 'S -> (OrderId * Order) list
    }

    module OrderStore =
        let inMemory () =
            { empty = OrderRepo.empty
              add = OrderRepo.add
              tryGet = OrderRepo.tryGet
              delete = OrderRepo.delete
              list = OrderRepo.list }
    
    let orderWithIdDto id order =
        {| id = OrderId.value id; lines = order.Lines.Length; total = Order.total order |}

    let orderWithIdToJson id order =
        (orderWithIdDto id order) |> Web.json

    let ordersToJson (ordersWithId: (OrderId * Order) list)  =
        ordersWithId
        |> List.map (fun (id, ord) -> orderWithIdDto id ord)
        |> Web.json
        
    module AppService =
        let createOrder store state lineItems =
            match Order.create lineItems with
            | Ok order ->
                let s', id = store.add order state
                Web.created (orderWithIdToJson id order), s'
            | Error e -> Web.badRequest e, state
        
        let getOrder store state id =
            match store.tryGet id state with
            | Some order -> Web.ok (orderWithIdToJson id order), state
            | None -> Web.notFound "not found", state
        
        let listOrders store state =
            let items = store.list state |> List.sortBy (fst >> OrderId.value)
            Web.ok (ordersToJson items), state
    
    module Migrations =
        let run cs =
            async {
                
            }