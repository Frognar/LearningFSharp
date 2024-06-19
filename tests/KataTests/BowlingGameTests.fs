module KataTests.BowlingGameTests

open Xunit
open Kata.BowlingGame

[<Fact>]
let ``score 0 for all zeros`` () =
    Assert.Equal(0, score (0 |> List.replicate 20) )

[<Fact>]
let ``score 20 for all ones`` () =
    Assert.Equal(20, score (1 |> List.replicate 20) )

[<Fact>]
let ``score spare`` () =
    Assert.Equal(18, score (List.concat [[5; 5; 4 ]; 0 |> List.replicate 17] ))

[<Fact>]
let ``partition open frames`` () =
    Assert.StrictEqual([ [ 1; 2 ]; [ 3; 4 ] ], splitFrames [ 1; 2; 3; 4 ])

[<Fact>]
let ``partition spare frames`` () =
    Assert.StrictEqual([ [ 5; 5; 4 ]; [ 4; 0 ] ], splitFrames [ 5; 5; 4; 0 ])