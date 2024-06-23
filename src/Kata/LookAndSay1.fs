module Kata.LookAndSay1

let lookAndSay number =
    match String.length number with
    | 1 -> "1" + number
    | _ when number[0] = number[1] -> "2" + string number[0]
    | _ -> "1" + string number[0] + "1" + string number[1]
