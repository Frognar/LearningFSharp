module App.Tests.Day03_Tests

open Xunit
open App.Domain


[<Fact>]
let ``rmap maps Ok and leaves Error`` () =
    let ok = Ok 10 |> Rop.rmap (fun x -> x * 2)
    let err = (Error "boom") |> Rop.rmap (fun (_:int) -> 0)
    Assert.Equal(Ok 20, ok)
    Assert.Equal(Error "boom", err)