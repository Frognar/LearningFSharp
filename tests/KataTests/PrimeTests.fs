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

[<Fact>]
let ``Primes up to 4 should be [ 2; 3 ]`` () =
    Assert.StrictEqual([ 2; 3 ], primesUpTo 4)

[<Fact>]
let ``Primes up to 5 should be [ 2; 3; 5 ]`` () =
    Assert.StrictEqual([ 2; 3; 5 ], primesUpTo 5)

[<Fact>]
let ``Primes up to 15 should be [ 2; 3; 5; 7; 11; 13 ]`` () =
    Assert.StrictEqual([ 2; 3; 5; 7; 11; 13 ], primesUpTo 15)
[<Fact>]
let ``Primes up to 100 should be [ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71; 73; 79; 83; 89; 97 ]`` () =
    Assert.StrictEqual([ 2; 3; 5; 7; 11; 13; 17; 19; 23; 29; 31; 37; 41; 43; 47; 53; 59; 61; 67; 71; 73; 79; 83; 89; 97 ], primesUpTo 100)

[<Fact>]
let ``Remove duplicates of first [ 2 ] should be []`` () =
    Assert.StrictEqual([], removeDuplicatesOfFirst [ 2 ])

[<Fact>]
let ``Remove duplicates of first [ 2; 3 ] should be [ 3 ]`` () =
    Assert.StrictEqual([ 3 ], removeDuplicatesOfFirst [ 2; 3 ])

[<Fact>]
let ``Remove duplicates of first [ 2; 3; 4 ] should be [ 3 ]`` () =
    Assert.StrictEqual([ 3 ], removeDuplicatesOfFirst [ 2; 3; 4 ])

[<Fact>]
let ``Remove duplicates of first [ 3; 5; 7; 9; 11 ] should be [ 5; 7; 11 ]`` () =
    Assert.StrictEqual([ 5; 7; 11 ], removeDuplicatesOfFirst [ 3; 5; 7; 9; 11 ])