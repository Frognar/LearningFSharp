module Kata.PrimeFactors0

let factorOf n =
    let rec factorize x div factors =
        match x with
        | _ when x < 2 -> factors |> List.rev
        | _ when x % div = 0 -> factorize (x / div) div (div :: factors)
        | _ -> factorize x (div + 1) factors
    
    factorize n 2 []