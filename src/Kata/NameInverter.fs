module Kata.NameInverter

open System

let isHonorific (candidate: string) =
    let knownHonorifics = [ "Mr." ]
    List.contains candidate knownHonorifics

let invert (name: string) =
    match name.Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.toList with
    | [ first ] -> first
    | [ first; last ] -> last + ", " + first
    | [ honorific; first; last ] when isHonorific honorific -> last + ", " + first
    | first :: last :: tail -> tail |> (List.fold (fun s v -> s + " " + v) (last + ", " + first))
    | _ -> ""
    