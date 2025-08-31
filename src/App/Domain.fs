namespace App

open System.Text.RegularExpressions

module Domain =
    type Contact =
        | Email of string
        | Phone of string

    type Person = { Name: string; Contact: Contact }

    let trim (str: string) =
        str.Trim()

    let normalizeWhitespace (str: string) =
        Regex.Replace(str, " +", " ") |> trim

    let normalizeName (name: string) =
        name |> normalizeWhitespace

    let normalizePhone (phone: string) =
        phone |> String.filter System.Char.IsDigit

    let normalizeContact contact : Contact =
        match contact with
        | Email e -> Email e
        | Phone p -> Phone (normalizePhone p)

    let tryCreateEmail (candidate: string) =
        match candidate |> Seq.contains '@' with
        | true -> Ok(Email candidate)
        | _ -> Error "Invalid email"

    let formatContactMethod = function
        | Email e -> "email", e
        | Phone p -> "phone", normalizePhone p

    let formatContact {Name = name; Contact = contact} =
        let label, value = formatContactMethod contact
        $"{name} can be contacted via {label}: {value}"

    let withContact (contact: Contact) (person: Person) =
        { person with
            Name = person.Name |> normalizeName
            Contact = contact |> normalizeContact }