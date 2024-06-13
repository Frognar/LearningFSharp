module ProjectZero.PrimeFactors
   
let rec factorsOf (n : int) : List<int> =
    if n > 1
    then (
        if n % 2 = 0
        then List.concat [[ 2 ]; factorsOf (n / 2) ]
        else (
            if n % 3 = 0
            then List.concat [[ 3 ]; factorsOf (n / 3) ]
            else [n]))
    else []