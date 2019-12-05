module Input

open System
open System.IO
open System.Reflection

let parse (raw: string) =
  raw.Split(',')
  |> Seq.map Int32.Parse
  |> Seq.toArray

let load() =
  let path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Input.txt")
  File.ReadAllText(path) |> parse