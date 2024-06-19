module Kata.BowlingGame

let (|IsSpare|_|) rolls =
    match rolls with
    | first :: second :: rest when first + second = 10 -> Some ([ first; second; rest[0] ], rest)
    | _ -> None

let (|IsStrike|_|) rolls =
    match rolls with
    | first :: rest when first = 10 -> Some (List.take 3 rolls, rest)
    | _ -> None

let splitFrames rolls =
    let rec loop result list =
        match list with
        | IsStrike (frame, rest) -> loop (frame :: result) rest
        | IsSpare (frame, rest) -> loop (frame :: result) rest
        | first::second::rest -> loop ([ first; second ] :: result) rest
        | _ -> result |> List.rev
    loop [] rolls

let score (rolls: List<int>) =
    rolls |> splitFrames |> List.sumBy List.sum