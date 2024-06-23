module Kata.LookAndSay1

let lookAndSay number =
    match String.length number with
    | 1 -> "1" + number
    | _ -> "2" + string number[0]
