module Kata.BowlingGame

let splitFrames rolls =
    let rec loop result list =
        match list with
        | first::second::rest when first + second = 10-> loop ([ first; second; rest[0] ] :: result) rest
        | first::second::rest -> loop ([ first; second ] :: result) rest
        | _ -> result |> List.rev
    loop [] rolls

let score (rolls: List<int>) =
    rolls |> List.sum