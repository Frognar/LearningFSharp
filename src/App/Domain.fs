namespace App

module Domain =
    let normalizePhone (phone : string) =
        phone |> String.filter System.Char.IsDigit
        