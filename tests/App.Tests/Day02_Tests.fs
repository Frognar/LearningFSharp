module Day02_Tests

open Xunit
open App.Domain

[<Fact>]
let ``map' squares numbers preserving order`` () =
    let input = [ 1; 2; 3; 4 ]
    let result = map' (fun x -> x * x) input
    Assert.Equal<int list>([ 1; 4; 9; 16 ], result)

[<Fact>]
let ``filter' keeps only even numbers preserving order`` () =
    let input = [ 5; 2; 4; 3; 8; 1 ]
    let result = filter' (fun x -> x % 2 = 0) input
    Assert.Equal<int list>([ 2; 4; 8 ], result)

[<Fact>]
let ``reduceOption sums non-empty list`` () =
    let input = [ 1; 2; 3; 4 ]
    let result = reduceOption (+) input
    Assert.Equal(Some 10, result)

[<Fact>]
let ``reduceOption is None for empty list`` () =
    let result = reduceOption (+) ([]: int list)
    Assert.Equal(None, result)

[<Fact>]
let ``average calculates mean for floats`` () =
    let input = [ 1.0; 2.0; 3.0; 4.0 ]
    let result = average input
    Assert.Equal(Some 2.5, result)

[<Fact>]
let ``average is None for empty list`` () = Assert.Equal(None, average [])

[<Fact>]
let ``median odd count picks middle after sort`` () =
    let input = [ 9.0; 1.0; 5.0 ]
    let result = median input
    Assert.Equal(Some 5.0, result)

[<Fact>]
let ``median even count averages two middles`` () =
    let input = [ 7.0; 1.0; 3.0; 9.0 ]
    let result = median input
    Assert.Equal(Some 5.0, result)

[<Fact>]
let ``variancePop population variance computed correctly`` () =
    let input = [2.0;4.0;4.0;4.0;5.0;5.0;7.0;9.0]
    let result = variancePop input
    Assert.Equal(Some 4.0, result)

[<Fact>]
let ``countBy groups by parity`` () =
    let input = [1;2;3;4;5;6]
    let asKey x = if x % 2 = 0 then "even" else "odd"
    let result = countBy asKey input
    Assert.Equal(3, result.["odd"])
    Assert.Equal(3, result.["even"])

type User = { Id:int; Name:string }
[<Fact>]
let ``distinctBy keeps first occurrence and preserves order`` () =
    let input =
        [ {Id=1;Name="A"}; {Id=2;Name="B"}; {Id=1;Name="A-dup"}; {Id=3;Name="C"}; {Id=2;Name="B-dup"} ]
    let result = distinctBy (fun u -> u.Id) input
    Assert.Equal<User list>(
        [ {Id=1;Name="A"}; {Id=2;Name="B"}; {Id=3;Name="C"} ],
        result
    )

[<Fact>]
let ``wordCount handles punctuation and casing and Polish letters`` () =
    let text = "Ala ma kota! ala, ma;   KOTA?  Żółć żółć."
    let wc = wordCount text
    Assert.Equal(2, wc.["ala"])
    Assert.Equal(2, wc.["ma"])
    Assert.Equal(2, wc.["kota"])
    Assert.Equal(2, wc.["żółć"])
    Assert.True(wc.ContainsKey "ala")
    Assert.False(wc.ContainsKey "Ala")    

[<Fact>]
let ``wordCount ignores empty tokens and multiple spaces`` () =
    let text = "   ...   Ala    ---  "
    let wc = wordCount text
    Assert.Equal(1, wc.["ala"])
    Assert.Equal(1, wc.Count)


[<Fact>]
let ``topNWords sorts by count desc then word asc`` () =
    let m =
        [ "ala",3; "ma",3; "kota",2; "żółć",2; "pies",2; "x",1 ]
        |> Map.ofList
    let result = topNWords m 5
    Assert.Equivalent([ ("ala",3); ("ma",3); ("kota",2); ("pies",2); ("żółć",2) ], result)

[<Fact>]
let ``topNWords fewer than requested returns all`` () =
    let m = [ "a",2; "b",1 ] |> Map.ofList
    let result = topNWords m 10
    Assert.Equivalent([("a",2);("b",1)], result)


[<Fact>]
let ``topWordsFromText returns top 3 words from text`` () =
    let text = "Ala ma kota. Ala nie ma psa, ale ma kota!"
    let result = topWordsFromText 3 text
    Assert.Equivalent([("ma",3);("ala",2);("kota",2)], result)


[<Fact>]
let ``wordCount with numbers keeps alphanumeric tokens`` () =
    let text = "R2D2 i C3PO to roboty. r2d2!"
    let wc = wordCount text
    Assert.Equal(2, wc.["r2d2"])
    Assert.Equal(1, wc.["c3po"])

[<Fact>]
let ``median and variancePop on singleton`` () =
    let input = [42.0]
    Assert.Equal(Some 42.0, median input)
    Assert.Equal(Some 0.0, variancePop input)

[<Fact>]
let ``countBy on empty yields empty map`` () =
    let result = countBy id ([]: int list)
    Assert.Equal(0, result.Count)