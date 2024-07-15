module Kata.FizzBuzz1

open System.Diagnostics

let render x =
    match x with
    | 3 -> "Fizz"
    | 5 -> "Buzz"
    | 6 -> "Fizz"
    | _ -> x.ToString()