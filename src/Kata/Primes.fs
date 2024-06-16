module Kata.Primes

let removeDuplicatesOfFirst list =
    match list with
    | first :: tail -> tail |> List.filter (fun x -> (x % first) <> 0)
    | _ -> []

let primesUpTo n =
    let rec loop x primes =
        match x with
        | _ when x < 2  -> primes
        | _ -> loop (x - 1) (x :: primes)
    loop n []