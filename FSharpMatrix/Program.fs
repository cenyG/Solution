open System
open System.Diagnostics

let runProc filename args timeout= 
    let procStartInfo = 
        ProcessStartInfo(
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            FileName = filename,
            Arguments = args
        )

    let outputs = System.Collections.Generic.List<string>()
    let errors = System.Collections.Generic.List<string>()
    let outputHandler f (_sender:obj) (args:DataReceivedEventArgs) = f args.Data

    let p = new Process(StartInfo = procStartInfo)
    p.OutputDataReceived.AddHandler(DataReceivedEventHandler (outputHandler outputs.Add))
    p.ErrorDataReceived.AddHandler(DataReceivedEventHandler (outputHandler errors.Add))

    let started = p.Start()
    if not started then
        failwithf "Failed to start process %s" filename
    
    p.BeginOutputReadLine()
    p.BeginErrorReadLine()
    p.WaitForExit(timeout) |> ignore
   
  
    printfn "Process %s finished" filename
    let cleanOut l = l |> Seq.filter (fun o -> String.IsNullOrEmpty o |> not)
    cleanOut outputs, cleanOut errors


[<EntryPoint>]
let main argv =
    try
        let (o, e) = runProc "python" "1" 1000
        o |> Seq.iter (printfn "%s ")
        e |> Seq.iter (printfn "%s ")
        0

    with e -> 
        printfn "%A" e
        -1
