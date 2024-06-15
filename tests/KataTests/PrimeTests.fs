module KataTests.PrimeTests

open Xunit
open Kata.Primes

[<Fact>]
let ``Primes up to 1 should be []`` () =
    Assert.StrictEqual([], primesUpTo 1)