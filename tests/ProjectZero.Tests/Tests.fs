module Tests

open ProjectZero.Calculator
open Xunit

[<Fact>]
let ``Add should correctly sum two numbers`` () =
    let a = 5
    let b = 3
    
    let result = add a b
    
    Assert.Equal(5 + 3, result)
