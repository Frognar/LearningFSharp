namespace App

open System.Text.RegularExpressions

module Domain =
    let normalizePhone (phone : string) =
        phone |> String.filter System.Char.IsDigit
    
    [<Struct>]
    type Email = Email of string
    
    let tryCreateEmail (candidate: string) =
        match candidate |> Seq.contains '@' with
        | true -> Ok (Email candidate)
        | _ -> Error "Invalid email"
    
    let trim (str: string) =
        str.Trim()

    let normalizeWhitespace (str: string) =
        Regex.Replace(str, " +", " ") |> trim

    let normalizeName (name: string) =
        name |> normalizeWhitespace