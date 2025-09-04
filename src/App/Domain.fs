namespace App

open System
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
            match a with
            | Ok x -> match b with
                      | Ok y -> Ok (f x y)
                      | Error y -> Error y
            | Error x -> Error x
        
        let apply f a =
            match f with
            | Ok f -> match a with
                      | Ok x -> Ok (f x)
                      | Error x -> Error x
            | Error f -> Error f
        
        let sequence xs =
            (Ok [], xs)
            ||> List.fold (map2 (fun s v -> s@[v]))
        
        let traverse f xs =
            xs |> map' f |> sequence
    
    let parseInt (candidate: string) =
        match Int32.TryParse candidate with
        | true, value -> Ok value
        | false, _ -> Error "Not an int"
    
    let sumInts candidates =
        candidates
        |> map' parseInt
        |> Res.sequence
        |> Rop.rmap (fun xs -> xs |> List.sum)