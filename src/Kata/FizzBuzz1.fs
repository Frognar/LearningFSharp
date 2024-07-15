module Kata.FizzBuzz1

let (|IsMultOf3|_|) x =
    match x with
    | _ when x % 3 = 0 -> Some ()
    | _ -> None

let render x =
    match x with
    | IsMultOf3 -> "Fizz"
    | 5 -> "Buzz"
    | _ -> x.ToString()