module Kata.LookAndSay

let lookAndSay n =
    if String.length n > 1 then
        if n[0] = n[1] then "2" + (string n[0])
        else "1" + string n[0] + "1" + string n[1]
    else
        "1" + n
