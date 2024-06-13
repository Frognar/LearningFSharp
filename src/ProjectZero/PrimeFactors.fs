module ProjectZero.PrimeFactors

let removeMultipliesOfFirst list =
    match list with
    | first :: rest -> List.filter ((fun n x -> x % n <> 0) first) rest
    | _ -> list

let primesUpTo (n: int) : List<int> =
    if n < 2 then []
    else
    let rec loop candidates primes =
        if List.isEmpty candidates
        then primes
        else loop (removeMultipliesOfFirst candidates) ((List.head candidates) :: primes)
    loop [2..n] []

let factorsOf (n: int) : List<int> =
    let rec loop n div res =
        if n > 1 then (
            if n % div = 0
            then loop (n / div) div (div :: res)
            else loop n (div + 1) res)
        else res
    loop n 2 []
