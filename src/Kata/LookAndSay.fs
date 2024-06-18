module Kata.LookAndSay

let lookAndSay n =
    if String.length n > 1 then
        let rec loop result count (input: string) =
            if String.length input > 1 then
                if input[0] = input[1] then
                    loop result (count + 1) input[1..]
                else
                    loop (result + (string count + string input[0])) 1 input[1..]
            else
                result + (string count + input)
                
        loop "" 1 n
    else
        "1" + n