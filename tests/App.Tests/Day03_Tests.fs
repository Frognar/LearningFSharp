module App.Tests.Day03_Tests

open Xunit
open App.Domain


[<Fact>]
let ``rmap maps Ok and leaves Error`` () =
    let ok = Ok 10 |> Rop.rmap (fun x -> x * 2)
    let err = (Error "boom") |> Rop.rmap (fun (_:int) -> 0)
    Assert.Equal(Ok 20, ok)
    Assert.Equal(Error "boom", err)

[<Fact>]
let ``rbind binds Ok and short-circuits Error`` () =
    let parsePositive x =
        if x > 0 then Ok (x * 3) else Error "non-positive"

    let a = Ok 5 |> Rop.rbind parsePositive
    let b = Ok 0 |> Rop.rbind parsePositive
    let c = (Error "nope") |> Rop.rbind parsePositive

    Assert.Equal(Ok 15, a)
    Assert.Equal(Error "non-positive", b)
    Assert.Equal(Error "nope", c)

[<Fact>]
let ``rmapError transforms only error`` () =
    let a = Ok "v" |> Rop.rmapError (fun _ -> "X")
    let b = (Error "bad") |> Rop.rmapError (fun e -> $"ERR:{e}")
    Assert.Equal(Ok "v", a)
    Assert.Equal(Error "ERR:bad", b)