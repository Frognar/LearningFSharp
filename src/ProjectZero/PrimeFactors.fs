module ProjectZero.PrimeFactors

let rec factorsOf (n: int) : List<int> =
    if n > 1 then
        (match n with
         | _ when n % 2 = 0 -> List.concat [ [ 2 ]; factorsOf (n / 2) ]
         | _ when n % 3 = 0 -> List.concat [ [ 3 ]; factorsOf (n / 3) ]
         | _ -> [ n ])
    else []
