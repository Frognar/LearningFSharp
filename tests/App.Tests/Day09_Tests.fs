module App.Tests.Day09_Tests

open Xunit
open App.Domain

[<Fact>]
let ``fromError maps Validation to 400`` () =
    let r = Web.fromError (Validation "Bad data")
    Assert.Equal(400, r.Status)
    Assert.Contains("Bad data", r.Body)