module KataTests.BowlingGameTests

open Xunit
open Kata.BowlingGame

[<Fact>]
let ``score 0 for all zeros`` () =
    Assert.Equal(0, score (0 |> List.replicate 20) )