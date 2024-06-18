module Kata.LookAndSay

let (|FirstTwoEquals|_|) (s: string) =
    if s[0] = s[1] then Some()
    else None

let (|SingleChar|_|) s = if String.length s = 1 then Some() else None    

let lookAndSay n =
    if String.length n = 1 then "1" + n
    else
        let rec loop result count (input: string) =
            match input with
            | SingleChar -> result + (string count + input)
            | FirstTwoEquals -> loop result (count + 1) input[1..]
            | _ -> loop (result + (string count + string input[0])) 1 input[1..]
                
        loop "" 1 n
