namespace ProjectZero

module Calculator =
    let add a b = a + b

    let rec fib n =
        match n with
        | v when v < 0 -> None
        | 0 -> Some(0)
        | 1 -> Some(1)
        | _ -> ((fib (n - 1)), (fib (n - 2))) ||> Option.map2 (fun x y -> x + y) 
