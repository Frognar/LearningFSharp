module Kata.LookAndSay

let lookAndSay n =
    if String.length n > 1 then
        let rec loop index count (input: string) =
            if input[index] = input[index + 1] then
                string (count + 1) + string (input[index + 1])
            else
                string count + string (input[index]) + string count + string (input[index + 1])
                
        loop 0 1 n
    else
        "1" + n