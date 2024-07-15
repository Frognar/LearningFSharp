module Kata.FizzBuzz1

open System.Diagnostics

let render x =
    match x with
    | _ when x % 3 = 0 -> "Fizz"
    | 5 -> "Buzz"
    | _ -> x.ToString()