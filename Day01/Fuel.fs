module Day01.Fuel
open System

let amountFromMass mass =
  mass / 3 - 2
  
let rec amountWithFuelMassFactoredIn mass =
  let amount = amountFromMass mass
  match amount with
  | x when x > 0 -> amount + amountWithFuelMassFactoredIn amount
  | _ -> 0 