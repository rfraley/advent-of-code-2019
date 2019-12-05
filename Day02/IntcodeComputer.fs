module IntcodeComputer

let private (|Add|Multiply|Halt|Unknown|) opcode =
  match opcode with
  | 1 -> Add
  | 2 -> Multiply
  | 99 -> Halt
  | _ -> Unknown

let rec executionLoop (i: int) (codes: int[]) =
  let executeOp (op: int -> int -> int) =
    let result = op codes.[codes.[i+1]] codes.[codes.[i+2]]
    codes.[codes.[i+3]] <- result
    executionLoop (i+4) codes
    
  match codes.[i] with
  | Add -> executeOp (+)
  | Multiply -> executeOp (*)
  | Halt -> codes
  | Unknown -> failwithf "Unknown opcode: %d" codes.[i] 

let execute (noun: int) (verb: int) (codes: int array) =
  codes.[1] <- noun
  codes.[2] <- verb
  codes |> executionLoop 0
  
let executeUntil (target: int) (codes: int array) =
  seq {
    for noun = 0 to 99 do
      for verb = 0 to 99 do
        yield noun, verb
  }
  |> Seq.find (fun (noun, verb) -> target = (execute noun verb (Array.copy codes) |> Array.item 0))
  
let partOne() =
  Input.load()
  |> execute 12 2
  |> Seq.item 0
  
let partTwo() =
  let noun, verb = Input.load() |> executeUntil 19690720
  100 * noun + verb