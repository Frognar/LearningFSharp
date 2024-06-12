namespace ProjectZero

module Calculator =
    let add a b = a + b

    let rec fib n =
        match n with
        | 0 -> 0
        | 1 -> 1
        | _ -> (fib (n - 1)) + (fib (n - 2))
