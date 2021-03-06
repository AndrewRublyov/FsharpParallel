module Threads

open System.Threading
open System.IO
open System.Diagnostics

let writeFile num () = 
  File.WriteAllText("Files/" + string num + ".txt", "Hello from F#, file #" + string num)

let executeExample filesCount =
  printfn "Start processing of %i files" filesCount
  Directory.CreateDirectory("Files") |> ignore
  let watch1 = Stopwatch()
  watch1.Start()
  for i in [1..filesCount] do writeFile i ()
  watch1.Stop()
  printfn "Write files task, synchronous\t%A" watch1.Elapsed

  let watch2 = Stopwatch()
  watch2.Start()
  let threads = List.init filesCount (fun i -> new Thread(writeFile i))
  for thread in threads do thread.Start()
  watch2.Stop()
  for thread in threads do thread.Join()
  printfn "Write files task, threads:\t%A" watch2.Elapsed