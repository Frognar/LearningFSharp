module Kata.NameInverter

open System

let invert (name: string) =
    match name.Split ' ' |> Array.where (fun n -> n.Length > 0) with
    | [| first |] -> first
    | [| first; last |] -> last + ", " + first
    | _ -> ""
    