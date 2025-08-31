namespace App

open System.Text.RegularExpressions

module Domain =
    type Contact =
        | Email of string
        | Phone of string

    type Person = { Name: string; Contact: Contact }

    let normalizePhone (phone: string) =
        phone |> String.filter System.Char.IsDigit

    let tryCreateEmail (candidate: string) =
        match candidate |> Seq.contains '@' with
        | true -> Ok(Email candidate)
        | _ -> Error "Invalid email"

    let trim (str: string) = str.Trim()

    let normalizeWhitespace (str: string) = Regex.Replace(str, " +", " ") |> trim

    let normalizeName (name: string) = name |> normalizeWhitespace

    let formatContact (person: Person) =
        match person.Contact with
        | Email e -> person.Name + " can be contacted via email: " + e
        | Phone p -> person.Name + " can be contacted via phone: " + normalizePhone p