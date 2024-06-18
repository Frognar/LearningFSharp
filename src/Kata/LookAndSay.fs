module Kata.LookAndSay

let lookAndSay n =
    if String.length n > 1 then
        let index = 0
        let current = n[index]
        let next = n[index + 1]
        if current = next then "2" + string current
        else "1" + string current + "1" + string next
    else
        "1" + n