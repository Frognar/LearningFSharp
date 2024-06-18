module Kata.LookAndSay

let lookAndSay n =
    if String.length n = 1 then (string 1) + n
    else (string 2) + (string n[0])