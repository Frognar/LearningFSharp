module Kata.Primes

let removeMultiplesOfFirst list =
    match list with
    | first :: tail -> tail |> List.filter (fun x -> (x % first) <> 0)
    | _ -> []

let primesUpTo n =
    let rec loop candidates primes =
        match candidates with
        | first :: _ -> loop (removeMultiplesOfFirst candidates) (first :: primes)
        | _ -> primes |> List.rev
    loop [2..n] []