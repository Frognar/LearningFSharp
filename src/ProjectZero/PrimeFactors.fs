module ProjectZero.PrimeFactors

let factorsOf (n: int) : List<int> =
    let rec loop n div res =
        if n > 1 then (
            if n % div = 0
            then loop (n / div) div (List.concat [ [ div ]; res ])
            else loop n (div + 1) res)
        else res
    loop n 2 []
