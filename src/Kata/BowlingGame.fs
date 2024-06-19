module Kata.BowlingGame

let splitFrames rolls =
    rolls |> List.chunkBySize 2

let score (rolls: List<int>) =
    rolls |> List.sum