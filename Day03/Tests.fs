module Tests

open Day03
open Swensen.Unquote
open Xunit

let createLine x1 y1 x2 y2 startCount endCount = {
  Start = {X=x1; Y=y1}
  End = {X=x2; Y=y2}
  StartStepCount = startCount
  EndStepCount = endCount
}

[<Theory>]
[<InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 159)>]
[<InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)>]
let ``Shortest distance for sample paths`` (firstWirePath, secondWirePath, expectedDistance) =
  let result = Day03.closestIntersectionDistance firstWirePath secondWirePath
  test <@ result = expectedDistance @>

[<Fact>]
let ``Converts up step`` () =
  test <@ Day03.toPathStep "U10" = { Direction = Up; Amount = 10 } @>
  
[<Fact>]
let ``Converts down step`` () =
  test <@ Day03.toPathStep "D123" = { Direction = Down; Amount = 123 } @>
  
[<Fact>]
let ``Converts left step`` () =
  test <@ Day03.toPathStep "L1" = { Direction = Left; Amount = 1 } @>
  
[<Fact>]
let ``Converts right step`` () =
  test <@ Day03.toPathStep "R456" = { Direction = Right; Amount = 456 } @>
  
[<Theory>]
[<InlineData("U10", 0, 10, 0, 10)>]
[<InlineData("D4", 0, -4, 0, 4)>]
[<InlineData("L21", -21, 0, 0, 21)>]
[<InlineData("R111", 111, 0, 0, 111)>]
let ``Line from single step`` (rawStep: string, expectedX: float, expectedY: float, expectedStartStepCount, expectedEndStepCount) =
  let expectedLines = createLine 0. 0. expectedX expectedY expectedStartStepCount expectedEndStepCount |> Array.singleton
  let result = Day03.toPathStep rawStep |> Seq.singleton |> Day03.mapSegments |> Seq.toArray
  test <@ result = expectedLines @>
  
[<Fact>]
let ``No intersection when lines are parallel in Y direction`` () =
  let firstLines = seq { yield  createLine 0. 0. 0. 40. 0 40 }
  let secondLines = seq { yield createLine 0. 0. 0. 15. 0 15 }
  let result = Day03.intersections firstLines secondLines
  test <@ Seq.isEmpty result @>
  
[<Fact>]
let ``No intersection when lines are parallel in X direction`` () =
  let firstLines = seq { yield createLine 0. 0. 15. 0. 0 15 }
  let secondLines = seq { yield createLine 0. 0. 40. 0. 0 50 }
  let result = Day03.intersections firstLines secondLines
  test <@ Seq.isEmpty result @>

[<Theory>]
[<InlineData(0., 0., 4., 2., 6)>]
[<InlineData(2., 4., 0., 0., 6)>]
[<InlineData(-30., 4., 0., 0., 34)>]
[<InlineData(0., 0., -30., -20., 50)>]
let ``Manhattan distance calculated between two points`` (p1X, p1Y, p2X, p2Y, expectedDistance) =
  let p1 = {X=p1X; Y=p1Y}
  let p2 = {X=p2X; Y=p2Y}
  let result = Day03.manhattanDistance p1 p2
  test <@ result = expectedDistance @>

[<Fact>]
let ``Part one solved`` () =
  test <@ Day03.partOne() = 1084 @>
  
[<Theory>]
[<InlineData("R75,D30,R83,U83,L12,D49,R71,U7,L72", "U62,R66,U55,R34,D71,R55,D58,R83", 610)>]
[<InlineData("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51", "U98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)>]
let ``Intersection with shortest step count found`` (firstWirePath, secondWirePath, expectedSteps) =
  let result = Day03.shortestIntersectionDistance firstWirePath secondWirePath
  test <@ result = expectedSteps @>
  
[<Fact>]
let ``Part two solved`` () =
  test <@ Day03.partTwo() = 9240 @>