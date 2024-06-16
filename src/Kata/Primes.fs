module Kata.Primes

let removeDuplicatesOfFirst list =
    List.tail list

let primesUpTo n =
    let rec loop x primes =
        match x with
        | _ when x < 2  -> primes
        | _ -> loop (x - 1) (x :: primes)
    loop n []