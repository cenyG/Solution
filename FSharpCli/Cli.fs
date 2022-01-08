module Cli

open System
open FSharpMatrix

let MAX_VALUE_INT = 1000
let MAX_VALUE_DOUBLE = 1000.0
let MAX_VALUE_BOOLEAN = true

let generateMatrix argv =
    let options =
        argv |> List.ofArray |> ArgsParser.parseArgsLine

    if options.help then
        printfn "MATRIX Generator Expert Supper Express v.0.1.1.0"
        printfn "Options to specify:"
        printfn "   /h /help        - print this manual"
        printfn "   /n /size        - matrix size"
        printfn "   /k /quantity    - quantity of generated matrices"
        printfn "   /t /type        - one of the following types (int, bool, double, extended)"
        printfn "   /p /path        - path were file will be saved"
        printfn "If option is omitted will create one with default value."
        printfn "Default values are: (size = 4, quantity = 1, type = int, path = './')"
    else
        for i = 1 to options.quantity do
            match options._type with
            | "int" ->
                Matrix.writeFile
                    (Matrix.stringify (
                        Matrix.generateMatrix options.size options.size MAX_VALUE_INT RandomGenerators.randomInt
                    ))
                    options.path
                    options._type
                    options.size
                    $"matrix_{i}"
                |> ignore
            | "bool" ->
                Matrix.writeFile
                    (Matrix.stringify (
                        Matrix.generateMatrix options.size options.size MAX_VALUE_BOOLEAN RandomGenerators.randomBool
                    ))
                    options.path
                    options._type
                    options.size
                    $"matrix_{i}"
                |> ignore
            | "double" ->
                Matrix.writeFile
                    (Matrix.stringify (
                        Matrix.generateMatrix options.size options.size MAX_VALUE_DOUBLE RandomGenerators.randomDouble
                    ))
                    options.path
                    options._type
                    options.size
                    $"matrix_{i}"
                |> ignore
            | "extended" ->
                Matrix.writeFile
                    (Matrix.stringify (
                        Matrix.generateMatrix
                            options.size
                            options.size
                            MAX_VALUE_DOUBLE
                            RandomGenerators.randomExtendedDouble
                    ))
                    options.path
                    options._type
                    options.size
                    $"matrix_{i}"
                |> ignore
            | _ -> raise (Exception("wrong type"))