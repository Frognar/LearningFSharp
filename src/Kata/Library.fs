module Kata.PrimeFactors0

let rec factorOf n =
    let rec factorize x div =
        match x with
        | _ when x < 2 -> []
        | _ when x % div = 0 -> div :: factorize (x / div) div
        | _ -> factorize x (div + 1)
    
    factorize n 2