module Kata.NameInverter

open System

let isHonorific (candidate: string) =
    let knownHonorifics = [ "Mr." ]
    List.contains candidate knownHonorifics

let invert (name: string) =
    match name.Split(' ', StringSplitOptions.RemoveEmptyEntries) with
    | [| first |] -> first
    | [| first; last |] -> last + ", " + first
    | [| honorific; first; last |] when isHonorific honorific -> last + ", " + first
    | [| first; last; postnominal |] -> last + ", " + first + " " + postnominal
    | _ -> ""
    