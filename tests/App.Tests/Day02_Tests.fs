module Day02_Tests

open Xunit
open App.Domain

[<Fact>]
let ``map' squares numbers preserving order`` () =
    let input = [ 1; 2; 3; 4 ]
    let result = map' (fun x -> x * x) input
    Assert.Equal<int list>([ 1; 4; 9; 16 ], result)

[<Fact>]
let ``filter' keeps only even numbers preserving order`` () =
    let input = [ 5; 2; 4; 3; 8; 1 ]
    let result = filter' (fun x -> x % 2 = 0) input
    Assert.Equal<int list>([ 2; 4; 8 ], result)

[<Fact>]
let ``reduceOption sums non-empty list`` () =
    let input = [ 1; 2; 3; 4 ]
    let result = reduceOption (+) input
    Assert.Equal(Some 10, result)

[<Fact>]
let ``reduceOption is None for empty list`` () =
    let result = reduceOption (+) ([]: int list)
    Assert.Equal(None, result)

[<Fact>]
let ``average calculates mean for floats`` () =
    let input = [ 1.0; 2.0; 3.0; 4.0 ]
    let result = average input
    Assert.Equal(Some 2.5, result)

[<Fact>]
let ``average is None for empty list`` () = Assert.Equal(None, average [])

[<Fact>]
let ``median odd count picks middle after sort`` () =
    let input = [ 9.0; 1.0; 5.0 ]
    let result = median input
    Assert.Equal(Some 5.0, result)

[<Fact>]
let ``median even count averages two middles`` () =
    let input = [ 7.0; 1.0; 3.0; 9.0 ]
    let result = median input
    Assert.Equal(Some 5.0, result)
