module Tests

open Swensen.Unquote
open System
open Xunit

[<Fact>]
let ``Inputs Loaded`` () =
    let inputs = Day01.Input.load()
    test <@ Seq.length inputs > 0 @>

[<Theory>]
[<InlineData(12, 2)>]
[<InlineData(14, 2)>]
[<InlineData(1969, 654)>]
[<InlineData(100756, 33583)>]
let ``Amount of fuel is based off of mass`` (mass: int, expectedFuel: int) =
  let result = Day01.Fuel.amountFromMass mass
  test <@ result = expectedFuel @>

[<Fact>]
let ``Sum of fuel from inputs`` () =
  let inputs = Day01.Input.load()
  let totalFuel = inputs |> Seq.map Day01.Fuel.amountFromMass |> Seq.sum
  test <@ totalFuel = 3511949 @>
  
[<Theory>]
[<InlineData(14, 2)>]
[<InlineData(1969, 966)>]
[<InlineData(100756, 50346)>]
let ``Mass of fuel accounted for`` (mass: int, expectedFuel: int) =
  test <@ Day01.Fuel.amountWithFuelMassFactoredIn mass = expectedFuel @>
  
[<Fact>]
let ``Final fuel sum`` () =
  let inputs = Day01.Input.load()
  let totalFuel = inputs |> Seq.map Day01.Fuel.amountWithFuelMassFactoredIn |> Seq.sum
  test <@ totalFuel = 5265045 @>