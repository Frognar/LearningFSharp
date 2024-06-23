module Kata.LookAndSay1

let (|Empty|_|) x =
    match x with
    | "" -> Some ()
    | _ -> None

let (|SingleChar|_|) x =
    match String.length x with
    | 1 -> Some x
    | _ -> None

let (|FirstTwoEquals|_|) x =
    match x with
    | Empty | SingleChar _ -> None
    | _ when x[0] = x[1] -> Some ()
    | _ -> None

let lookAndSay number =
    let rec loop x count result =
        match x with
        | Empty -> ""
        | SingleChar c -> result + string count + c
        | FirstTwoEquals -> loop x[1..] (count + 1) result
        | _ -> loop x[1..] 1 (result + string count + string x[0])
    
    loop number 1 ""

let lookAndSaySequence number depth =
    let rec loop x y result =
        match y with
        | 0 -> result
        | _ -> loop (lookAndSay x) (y - 1) (result + ":" + lookAndSay x)
   
    loop number depth number