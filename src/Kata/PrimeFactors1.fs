module Kata.PrimeFactors1

let factorsOf n =
    match n with
    | _ when n < 2 -> []
    | _ -> [ n ]