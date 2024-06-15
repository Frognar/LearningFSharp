module Kata.PrimeFactors1

let rec factorsOf n =
    match n with
    | _ when n < 2 -> []
    | _ when n % 2 = 0 -> 2 :: factorsOf (n / 2)
    | _ -> [ n ]