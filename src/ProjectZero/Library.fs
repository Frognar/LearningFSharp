namespace ProjectZero

module Calculator =
    let add a b = a + b

    let fib n =
        let rec loop acc1 acc2 n =
            match n with
            | 0 -> acc1
            | 1 -> acc2
            | _ -> loop acc2 (acc1 + acc2) (n - 1)

        match n with
        | v when v < 0 -> None
        | _ -> Some(loop 0 1 n) 
