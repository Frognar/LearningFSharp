namespace App

module Domain =
    let normalizePhone (phone : string) =
        phone |> String.filter System.Char.IsDigit
    
    [<Struct>]
    type Email = Email of string
    
    let tryCreateEmail (candidate: string) =
        match candidate |> Seq.contains '@' with
        | true -> Ok (Email candidate)
        | _ -> Error "Invalid email"