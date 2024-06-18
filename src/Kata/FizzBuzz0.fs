module Kata.FizzBuzz0

let render n =
    match n with
    | _ when n % 3 = 0 && n % 5 = 0 -> "FizzBuzz"
    | _ when n % 3 = 0 -> "Fizz"
    | _ when n % 5 = 0 -> "Buzz"
    | _ -> string n