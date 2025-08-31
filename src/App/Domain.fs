namespace App

module Domain =
    let normalizePhone (phone : string) =
        let chars = ['-'; ' '; '.'; '('; ')']
        phone |> String.collect (fun c -> if Seq.exists ((=) c) chars then "" else string c)
        