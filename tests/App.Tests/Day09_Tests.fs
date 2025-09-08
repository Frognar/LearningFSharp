module App.Tests.Day09_Tests

open Xunit
open App.Domain

[<Fact>]
let ``fromError maps Validation to 400`` () =
    let r = Web.fromError (Validation "Bad data")
    Assert.Equal(400, r.Status)
    Assert.Contains("Bad data", r.Body)

[<Fact>]
let ``fromError maps NotFound to 404`` () =
    let r = Web.fromError (NotFound "Order 123")
    Assert.Equal(404, r.Status)
    Assert.Contains("Order 123", r.Body)

[<Fact>]
let ``fromError maps Conflict to 409`` () =
    let r = Web.fromError (Conflict "Duplicate")
    Assert.Equal(409, r.Status)
    Assert.Contains("Duplicate", r.Body)

[<Fact>]
let ``fromError maps Unauthorized to 401`` () =
    let r = Web.fromError (Unauthorized "No token")
    Assert.Equal(401, r.Status)
    Assert.Contains("No token", r.Body)