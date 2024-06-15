module KataTests.PrimeTests

open Xunit
open Kata.Primes

[<Fact>]
let ``Primes up to 1 should be []`` () =
    Assert.StrictEqual([], primesUpTo 1)

[<Fact>]
let ``Primes up to 2 should be [ 2 ]`` () =
    Assert.StrictEqual([ 2 ], primesUpTo 2)