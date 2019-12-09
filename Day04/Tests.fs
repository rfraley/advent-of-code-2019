module Tests

open Swensen.Unquote
open Xunit
  
[<Theory>]
[<InlineData("121", false)>]
[<InlineData("112", true)>]
[<InlineData("211", true)>]
let ``Detects when password has adjacent digits that are the same`` (password: string, expected: bool) =
  test <@ (Day4.naiveHasAdjacentSameDigits password) = expected @>
  
[<Theory>]
[<InlineData("444", true)>]
[<InlineData("454", false)>]
[<InlineData("123", true)>]
let ``Detects when password digits never decrease`` (password: string, expected: bool) =
  test <@ (Day4.neverDecreases password) = expected @>
  
[<Fact>]
let ``Group consecutive digits`` () =
  let password = "1223334567777"
  let expected = [[1]; [2; 2]; [3; 3; 3]; [4]; [5]; [6]; [7; 7; 7; 7]]
  let result = Day4.groupConsecutiveDigits password
  test <@ result = expected @>

[<Fact>]
let ``Part one solved`` () =
  test <@ Day4.partOne() = 1150 @>
  
[<Fact>]
let ``Part two solved`` () =
  test <@ Day4.partTwo() = 748 @>
