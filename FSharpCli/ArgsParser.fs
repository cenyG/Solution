module ArgsParser

open System

type CommandLineOptions =
    { _type: string // type
      size: int // size
      path: string // file path
      quantity: int // number of matrix
      help: bool } // number of matrix

let defaultOptions =
    { _type = "int"
      size = 4
      path = Environment.CurrentDirectory
      quantity = 1
      help = false }

let rec private parseArgsInternal args optionsSoFar =
    match args with
    // empty list means we're done.
    | [] -> optionsSoFar

    // match type by flag
    | "/t" :: xs
    | "/type" :: xs ->
        //start a submatch on the next arg
        match xs with
        | "double" :: xss ->
            let newOptionsSoFar = { optionsSoFar with _type = "double" }
            parseArgsInternal xss newOptionsSoFar

        | "extended" :: xss ->
            let newOptionsSoFar = { optionsSoFar with _type = "extended" }
            parseArgsInternal xss newOptionsSoFar

        | "int" :: xss ->
            let newOptionsSoFar = { optionsSoFar with _type = "int" }
            parseArgsInternal xss newOptionsSoFar

        | "bool" :: xss ->
            let newOptionsSoFar = { optionsSoFar with _type = "bool" }
            parseArgsInternal xss newOptionsSoFar

        // handle unrecognized option and keep looping
        | _ ->
            eprintfn "wrong matrix type"
            parseArgsInternal xs optionsSoFar

    // match size
    | "/n" :: xs
    | "/size" :: xs ->
        //start a submatch on the next arg
        match xs with
        | _size :: xss ->
            let currentSize = _size |> int
            let newOptionsSoFar = { optionsSoFar with size = currentSize }
            parseArgsInternal xss newOptionsSoFar
        | [] ->
            eprintfn "oops something goes wrong"
            parseArgsInternal xs optionsSoFar

    // match path
    | "/p" :: xs
    | "/path" :: xs ->
        //start a submatch on the next arg
        match xs with
        | _path :: xss ->
            let newOptionsSoFar = { optionsSoFar with path = _path }
            parseArgsInternal xss newOptionsSoFar
        | [] ->
            eprintfn "oops something goes wrong"
            parseArgsInternal xs optionsSoFar

    // match path
    | "/k" :: xs
    | "/quantity" :: xs ->
        //start a submatch on the next arg
        match xs with
        | _quantity :: xss ->
            let currentQuantity = _quantity |> int

            let newOptionsSoFar =
                { optionsSoFar with
                      quantity = currentQuantity }

            parseArgsInternal xss newOptionsSoFar
        | [] ->
            eprintfn "oops something goes wrong"
            parseArgsInternal xs optionsSoFar

    | "/h" :: xs
    | "/help" :: xs ->
        let newOptionsSoFar = { optionsSoFar with help = true }
        parseArgsInternal xs newOptionsSoFar

    // handle unrecognized option and keep looping
    | x :: xs ->
        eprintfn $"Option '{x}' is unrecognized"
        parseArgsInternal xs optionsSoFar



let parseArgsLine args =
    parseArgsInternal args defaultOptions