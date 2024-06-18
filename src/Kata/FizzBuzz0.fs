module Kata.FizzBuzz0

let (|MultOf3|_|) i =
    match i with
    | _ when i % 3 = 0 -> Some MultOf3
    | _ -> None
    
let (|MultOf5|_|) i =
    match i with
    | _ when i % 5 = 0 -> Some MultOf5
    | _ -> None

let render n =
    match n with
    | MultOf3 & MultOf5 -> "FizzBuzz"
    | MultOf3 -> "Fizz"
    | MultOf5 -> "Buzz"
    | _ -> string n