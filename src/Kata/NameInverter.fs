module Kata.NameInverter

open System

let isHonorific (candidate: string) =
    let knownHonorifics = [ "Mr." ]
    List.contains candidate knownHonorifics

let withoutHonorifics nameParts =
    match nameParts with
    | candidate :: rest when isHonorific candidate -> rest
    | _ -> nameParts

let invert (name: string) =
    let nameParts = name.Split(' ', StringSplitOptions.RemoveEmptyEntries) |> Array.toList
    match nameParts |> withoutHonorifics with
    | [ first ] -> first
    | [ first; last ] -> last + ", " + first
    | first :: last :: tail -> tail |> (List.fold (fun s v -> s + " " + v) (last + ", " + first))
    | _ -> ""
    