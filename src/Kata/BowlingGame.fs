module Kata.BowlingGame

let rec splitFrames rolls =
    match rolls with
    | first::second::rest -> [ first; second ] :: splitFrames rest
    | _ -> []

let score (rolls: List<int>) =
    rolls |> List.sum