module Kata.FizzBuzz1

let (|IsMultOf3|_|) x =
    match x with
    | _ when x % 3 = 0 -> Some ()
    | _ -> None

let (|IsMultOf5|_|) x =
    match x with
    | _ when x % 5 = 0 -> Some ()
    | _ -> None

let render x =
    match x with
    | IsMultOf3 & IsMultOf5 -> "FizzBuzz"
    | IsMultOf3 -> "Fizz"
    | IsMultOf5 -> "Buzz"
    | _ -> x.ToString()

let fizzBuzz () =
    [1..100] |> List.map render