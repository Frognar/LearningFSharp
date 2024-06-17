module Kata.PrimeFactors2

let factorsOf n =
    match n with
    | _ when n < 2 -> []
    | _ -> [ 2 ]