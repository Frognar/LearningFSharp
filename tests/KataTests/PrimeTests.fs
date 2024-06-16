module KataTests.PrimeTests

open Xunit
open Kata.Primes

[<Fact>]
let ``Primes up to 1 should be []`` () =
    Assert.StrictEqual([], primesUpTo 1)

[<Fact>]
let ``Primes up to 2 should be [ 2 ]`` () =
    Assert.StrictEqual([ 2 ], primesUpTo 2)

[<Fact>]
let ``Primes up to 3 should be [ 2; 3 ]`` () =
    Assert.StrictEqual([ 2; 3 ], primesUpTo 3)

//[<Fact>]
//let ``Primes up to 4 should be [ 2; 3 ]`` () =
//    Assert.StrictEqual([ 2; 3 ], primesUpTo 4)

[<Fact>]
let ``Remove duplicates of first [ 2 ] should be []`` () =
    Assert.StrictEqual([], removeDuplicatesOfFirst [ 2 ])

[<Fact>]
let ``Remove duplicates of first [ 2; 3 ] should be [ 3 ]`` () =
    Assert.StrictEqual([ 3 ], removeDuplicatesOfFirst [ 2; 3 ])