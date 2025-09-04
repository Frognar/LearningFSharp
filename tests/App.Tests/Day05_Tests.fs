module App.Tests.Day05_Tests

open Xunit
open App.Domain

[<Fact>]
let ``map2 combines two Ok values`` () =
    let add (a: int) (b: int) = a + b
    let r = Res.map2 add (Ok 2) (Ok 3)
    Assert.Equal(Ok 5, r)

[<Fact>]
let ``map2 short-circuits on first Error`` () =
    let add (a: int) (b: int) = a + b
    let r1 = Res.map2 add (Error "A") (Ok 3)
    let r2 = Res.map2 add (Ok 2) (Error "B")
    Assert.Equal(Error "A", r1)
    Assert.Equal(Error "B", r2)
