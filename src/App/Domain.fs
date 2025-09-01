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
        let lst = map
                |> Map.toList
                |> List.sortBy snd
                |> List.rev
        let cnt = List.length lst
        lst |> List.take (min cnt n)
        