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

[<Fact>]
let ``apply lifts function in Result and applies to Ok`` () =
    let f = Ok (fun (s:string) -> s.Trim().Length)
    let r = Res.apply f (Ok "  Ala ")
    Assert.Equal(Ok 3, r)

[<Fact>]
let ``apply propagates Error from either side`` () =
    let fBad = Error "no fun"
    let vBad = Error "no val"
    let fOk  = Ok (fun x -> x + 1)
    Assert.Equal(Error "no fun", Res.apply fBad (Ok 1))
    Assert.Equal(Error "no val", Res.apply fOk vBad)

[<Fact>]
let ``sequence turns list of Ok into Ok list`` () =
    let r = Res.sequence [ Ok 1; Ok 2; Ok 3 ]
    Assert.Equal(Ok [1;2;3], r)

[<Fact>]
let ``sequence stops at first Error`` () =
    let r = Res.sequence [ Ok 1; Error "boom"; Ok 3 ]
    Assert.Equal(Error "boom", r)

[<Fact>]
let ``traverse applies function and sequences results`` () =
    let f x = if x > 0 then Ok (x * 2) else Error "neg"
    let rGood = Res.traverse f [1;2;3]
    let rBad  = Res.traverse f [1;-1;3]
    Assert.Equal(Ok [2;4;6], rGood)
    Assert.Equal(Error "neg", rBad)