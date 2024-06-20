module Kata.NameInverter

open System

let invert (name: string) =
    match name.Split ' ' with
    | [| first |] -> first
    | [| first; last |] -> last + ", " + first
    | _ -> ""
    