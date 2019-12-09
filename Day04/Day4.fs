module Day4
open System

let charToIntValue c = Char.GetNumericValue c |> int

let digitPairs (password: string) =
  password
  |> Seq.map charToIntValue
  |> Seq.pairwise
  
let neverDecreases =
  digitPairs >> (Seq.forall (fun (d1, d2) -> d2 >= d1))

let naiveHasAdjacentSameDigits =
  digitPairs >> (Seq.exists (fun (d1, d2) -> d1 = d2))

let groupConsecutiveDigits (password: string) =
  let folder (state: list<list<int>>) (digit: int) =
    let currentGroup = state.Head
    match currentGroup with
    | [] -> [digit] :: state.Tail
    | _ ->
      if currentGroup.Head = digit then
        (digit :: currentGroup) :: state.Tail
      else
        [ digit ] :: state
  password
  |> Seq.map charToIntValue
  |> Seq.fold folder [[]]
  |> List.rev
  
let hasAdjacentSameDigitsInSmallGroup = groupConsecutiveDigits >> Seq.exists (fun g -> g.Length = 2)

let validatePassword validators password = List.forall (fun v -> v password) validators  

let inputs = seq { 240298..784956 } |> Seq.map (fun i -> i.ToString())
let executePart validators =
  inputs
  |> Seq.filter (validatePassword validators)
  |> Seq.length
let partOne() = executePart [naiveHasAdjacentSameDigits; neverDecreases]
let partTwo() = executePart [hasAdjacentSameDigitsInSmallGroup; neverDecreases]