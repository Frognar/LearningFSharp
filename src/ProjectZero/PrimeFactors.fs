module ProjectZero.PrimeFactors

let removeMultipliesOfFirst list =
    match list with
    | first :: rest -> List.filter ((fun n x -> x % n <> 0) first) rest
    | _ -> list

let primesUpTo (n: int) : List<int> =
    let rec loop candidate primes =
        if candidate <= n
        then loop (candidate + 1) (List.concat [ primes; [candidate] ])
        else primes
    loop 2 []

let factorsOf (n: int) : List<int> =
    let rec loop n div res =
        if n > 1 then (
            if n % div = 0
            then loop (n / div) div (List.concat [ [ div ]; res ])
            else loop n (div + 1) res)
        else res
    loop n 2 []
