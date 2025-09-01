namespace App

open System.Collections.Generic
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

    type Person = private { Name: PersonName; Contact: Contact }
    module Person =
        let create name contact =
            { Name = name; Contact = contact }

        let withContact contact p =
            { p with Contact = contact }

        let format p =
            let name: string = PersonName.value p.Name
            match p.Contact with
            | Email e -> $"{name} can be contacted via email: {EmailAddress.value e}"
            | Phone p -> $"{name} can be contacted via phone: {PhoneNumber.value p}"

        let contact (p: Person) = p.Contact             

    let map' f xs =
        xs |> List.fold (fun acc v -> f v :: acc) [] |> List.rev
