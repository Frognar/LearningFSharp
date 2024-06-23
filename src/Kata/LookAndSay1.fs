module Kata.LookAndSay1

let lookAndSay number =
    let rec loop x count result =
        match String.length x with
        | 1 -> result + string count + x
        | _ when x[0] = x[1] -> loop x[1..] (count + 1) result
        | _ -> loop x[1..] 1 (result + string count + string x[0])
    
    loop number 1 ""
