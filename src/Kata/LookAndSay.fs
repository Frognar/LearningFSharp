module Kata.LookAndSay

let lookAndSay n =
    if String.length n > 1 then
        let current = n[0]
        let next = n[1]
        if current = next then "2" + string current
        else "1" + string current + "1" + string next
    else
        "1" + n