module Kata.FizzBuzz0

let render n =
    match n with
    | _ when n = 3 -> "Fizz"
    | _ -> string n