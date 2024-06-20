module Kata.NameInverter

open System

let invert (name: string) =
    match name.Split(' ', StringSplitOptions.RemoveEmptyEntries) with
    | [| first |] -> first
    | [| first; last |] -> last + ", " + first
    | [| _; first; last |] -> last + ", " + first
    | _ -> ""
    