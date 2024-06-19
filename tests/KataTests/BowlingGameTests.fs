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
let ``score strike`` () =
    Assert.Equal(18, score (List.concat [[10; 2; 2 ]; 0 |> List.replicate 17] ))

[<Fact>]
let ``score perfect game`` () =
    Assert.Equal(300, score (10 |> List.replicate 12))

[<Fact>]
let ``partition open frames`` () =
    Assert.StrictEqual([ [ 1; 2 ]; [ 3; 4 ] ], splitFrames [ 1; 2; 3; 4 ])

[<Fact>]
let ``partition spare frames`` () =
    Assert.StrictEqual([ [ 5; 5; 4 ]; [ 4; 0 ] ], splitFrames [ 5; 5; 4; 0 ])

[<Fact>]
let ``partition strike frames`` () =
    Assert.StrictEqual([ [ 10; 5; 4 ]; [ 5; 4 ] ], splitFrames [ 10; 5; 4 ])