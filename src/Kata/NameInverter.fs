module Kata.NameInverter

open System

let invert (name: string) =
    match name.Split(' ', StringSplitOptions.RemoveEmptyEntries) with
    | [| first |] -> first
    | [| first; last |] -> last + ", " + first
    | [| first; last; postnominal |] when postnominal = "Sr." -> last + ", " + first + " " + postnominal
    | [| _; first; last |] -> last + ", " + first
    | _ -> ""
    