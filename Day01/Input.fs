module Day01.Input
open System
open System.IO
open System.Reflection

let load() =
  let dir = Assembly.GetExecutingAssembly().Location |> Path.GetDirectoryName
  let inputFilePath = Path.Combine(dir, "Input.txt")
  File.ReadLines inputFilePath
  |> Seq.map Int32.Parse