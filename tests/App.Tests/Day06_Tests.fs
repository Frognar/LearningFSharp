module App.Tests.Day06_Tests

open Xunit
open App.Domain

[<Theory>]
[<InlineData(1)>]
[<InlineData(42)>]
let ``ProductId.create accepts positives`` (i) =
    match ProductId.create i with
    | Ok _ -> Assert.True(true)
    | Error e -> failwithf "unexpected: %s" e

[<Theory>]
[<InlineData(0)>]
[<InlineData(-5)>]
let ``ProductId.create rejects non-positives`` (i) =
    match ProductId.create i with
    | Error _ -> Assert.True(true)
    | Ok _ -> failwith "expected Error"