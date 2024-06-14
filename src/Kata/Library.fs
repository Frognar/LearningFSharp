module Kata.PrimeFactors0

let factorOf n =
    match n with
    | _ when n < 2 -> []
    | _ -> [ 2 ]