module Tests

open Xunit
open Swensen.Unquote

[<Theory>]
[<InlineData("1,0,0,0,99", "2,0,0,0,99")>]
[<InlineData("2,3,0,3,99", "2,3,0,6,99")>]
[<InlineData("2,4,4,5,99,0", "2,4,4,5,99,9801")>]
[<InlineData("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")>]
let ``Computer output matches expected`` (input: string, expectedOutput: string) =
  let result = Input.parse input |> IntcodeComputer.executionLoop 0
  let expected = Input.parse expectedOutput
  test <@ result = expected @>

[<Fact>]
let ``Solution 1 result`` () =
  let result = IntcodeComputer.partOne()
  test <@ result = 3765464 @>
  
[<Fact>]
let ``Solution 2 result`` () =
  let result = IntcodeComputer.partTwo()
  test <@ result = 7610 @>