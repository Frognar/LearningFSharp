module Kata.LookAndSay

let lookAndSay n =
    if String.length n > 1 then
        let index = 0
        let count = 1
        let current = n[index]
        let next = n[index + 1]
        if current = next then string (count + 1) + string current
        else string count + string current + string count + string next
    else
        "1" + n