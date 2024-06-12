namespace ProjectZero

module Calculator =
    let add a b =
        a + b
        
    let rec fib n =
        if n < 2 then n else (fib (n - 1)) + (fib (n - 2))