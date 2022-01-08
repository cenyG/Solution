open System
open System.IO
open System.Threading
open FSharpMatrix
open Mailboxes.Worker
open Mailboxes.Types
open Matrix

type CreateCommand = { Name: string; Size: int; Type: Type }

type ReadCommand =
    { Name: string
      FileName: string
      Type: Type
      Path: string }

type WriteCommand = { Name: string; Path: string }

type MultiplyCommand =
    { LeftName: string
      RightName: string
      Type: Type }

type FindTrcCommand = { Name: string }
type ExitCommand = unit -> unit

let intoMessage =
    printfn "MATRIX MailBoX Duper Commander Trial Version patch v.0.1.2.3"
    printfn "Commands:"
    printfn "   create  MATRIX_NAME TYPE SIZE"
    printfn "   read    MATRIX_NAME FILE_NAME TYPE PATH"
    printfn "   write   MATRIX_NAME PATH"
    printfn "   mul     LEFT_MATRIX_NAME RIGHT_MATRIX_NAME TYPE"
    printfn "   trc     MATRIX_NAME"
    printfn "   exit    "
    printfn "You can specify PATH as '.' then it will be your working directory"
    printfn "------------------------------------------------------------------"
    printfn "Examples: "
    printfn "   create m1 int 4"
    printfn "   create m2 int 4"
    printfn "   write m2 ."
    printfn "   read m3 m2 int ."
    printfn "   mul m3 m1 int"
    printfn "   trc m2"
    printfn "   exit"


let hackPath path =
    if path = "." then
        $"{Directory.GetCurrentDirectory()}"
    else
        path

let parseInput (input: string) : Map<string, string> =
    let cmd =
        input.Trim().Split(" ")
        |> Array.map (fun e -> e.Trim())

    match cmd with
    | [| "create"; name; _type; size |] ->
        [| ("Cmd", "create")
           ("Name", name)
           ("Type", _type)
           ("Size", size) |]
        |> Map.ofArray

    | [| "read"; name; fileName; _type; _path |] ->
        [| ("Cmd", "read")
           ("Name", name)
           ("FileName", fileName)
           ("Type", _type)
           ("Path", hackPath _path) |]
        |> Map.ofArray

    | [| "write"; name; _path |] ->
        [| ("Cmd", "write")
           ("Name", name)
           ("Path", hackPath _path) |]
        |> Map.ofArray

    | [| "mul"; left; right; _type |] ->
        [| ("Cmd", "mul")
           ("LeftName", left)
           ("RightName", right)
           ("Type", _type) |]
        |> Map.ofArray

    | [| "trc"; name |] ->
        [| ("Cmd", "trc"); ("Name", name) |]
        |> Map.ofArray

    | [| "exit" |] -> [| ("Cmd", "exit") |] |> Map.ofArray

    | _ -> raise (Exception("Wrong command"))



let sendCommand (mailbox: Worker) (input: Map<string, string>) =
    match input.["Cmd"] with
    | "create" ->
        let name = input.["Name"]

        let resp =
            mailbox.Create
                { Name = name
                  Size = input.["Size"] |> int
                  Type = getType (input.["Type"]) }

        if resp.Status = Status.Ok then
            printfn $"Matrix {name} was created:"
            printMatrix resp.matrixInstance.Content

    | "read" ->
        let name = input.["Name"]

        let resp =
            mailbox.Read
                { Name = name
                  FileName = input.["FileName"]
                  Type = getType (input.["Type"])
                  Path = input.["Path"] }

        if resp.Status = Status.Ok then
            printfn $"Matrix {name} read"
            printMatrix resp.matrixInstance.Content

    | "write" ->
        let name = input.["Name"] |> string

        let resp =
            mailbox.Write { Name = name; Path = input.["Path"] }

        if resp.Status = Status.Ok then
            printfn $"Matrix {name} wrote to file {resp.Path}"

    | "mul" ->
        let left = input.["LeftName"]
        let right = input.["RightName"]

        let resp =
            mailbox.Multiply
                { LeftName = left
                  RightName = right
                  Type = getType (input.["Type"]) }

        if resp.Status = Status.Ok then
            printfn $"Matrix {left} × {right} result:"
            printMatrix resp.matrixInstance.Content

    | "trc" ->
        let name = input.["Name"]
        let resp = mailbox.FindTrc { Name = name }

        if resp.Status = Status.Ok then
            printfn $"Matrix {name} transitive closure:"
            printMatrix resp.matrixInstance.Content
    | "exit" -> mailbox.Exit(fun () -> raise (IntendedExit("that's all folks")))
    | _ -> printfn "Wrong command"


let rec mainLoop mailbox =
    try
        let args = Console.ReadLine() |> parseInput
        sendCommand mailbox args

        if args.["Cmd"] = "exit" then
            Thread.Sleep(200)
            1
        else
            mainLoop mailbox
    with
    | e ->
        printfn $"{e.Message}"
        mainLoop mailbox

[<EntryPoint>]
let main argv =
    let mailbox = Worker()
    mailbox.OnError.Add(fun e -> printf $"Error: {e.Message}")

    mainLoop (mailbox)
