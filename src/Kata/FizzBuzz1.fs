module Kata.FizzBuzz1

open System.Diagnostics

let render x =
    match x with
    | 3 -> "Fizz"
    | 5 -> "Buzz"
    | _ -> x.ToString()