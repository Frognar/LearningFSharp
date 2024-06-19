module KataTests.BowlingGameTests

open Xunit
open Kata.BowlingGame

[<Fact>]
let ``score 0 for all zeros`` () =
    Assert.Equal(0, score (0 |> List.replicate 20) )

[<Fact>]
let ``score 20 for all ones`` () =
    Assert.Equal(20, score (1 |> List.replicate 20) )