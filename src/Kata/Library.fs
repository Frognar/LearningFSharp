module Kata.PrimeFactors0

let rec factorOf n =
    match n with
    | _ when n < 2 -> []
    | _ when n % 2 = 0 -> 2 :: factorOf (n / 2)
    | _ -> [ n ]