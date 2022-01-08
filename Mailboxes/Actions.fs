module Mailboxes.Actions

open System
open System.IO
open Mailboxes.Types


let createMatrix (cmd: CreateCommand) : MatrixInstance =
    let matrixStr = 
        match cmd.Type with
        | Int ->
            FSharpMatrix.Matrix.toStringMatrix (FSharpMatrix.Matrix.generateMatrix cmd.Size cmd.Size 1000 FSharpMatrix.RandomGenerators.randomInt)
        | Bool ->
            FSharpMatrix.Matrix.toStringMatrix (FSharpMatrix.Matrix.generateMatrix cmd.Size cmd.Size true FSharpMatrix.RandomGenerators.randomBool)
        | Double ->
            FSharpMatrix.Matrix.toStringMatrix (FSharpMatrix.Matrix.generateMatrix cmd.Size cmd.Size 1000.0 FSharpMatrix.RandomGenerators.randomDouble)
        | Extended ->
            FSharpMatrix.Matrix.toStringMatrix (FSharpMatrix.Matrix.generateMatrix cmd.Size cmd.Size 1000.0 FSharpMatrix.RandomGenerators.randomExtendedDouble)
                
    {Content = matrixStr; _type = cmd.Type; size = cmd.Size}
    
    
let readMatrix (cmd: ReadCommand) : MatrixInstance =
    let matrix = $"{cmd.Path}{Path.DirectorySeparatorChar}{cmd.FileName}" |> File.ReadAllText |> FSharpMatrix.Matrix.parseMatrix
    {Content = matrix; _type = cmd.Type; size = matrix.Length}
    
    
let writeMatrix (cmd: WriteCommand) (cache: Map<string, MatrixInstance>) =
    let mx = cache.[cmd.Name]
    let matrixStr = FSharpMatrix.Matrix.stringify mx.Content
    let path = $"{cmd.Path}{Path.DirectorySeparatorChar}{cmd.Name}"
    FSharpMatrix.Matrix.writePlain matrixStr path
    path
    
    
    
let _innerMultiplyMatrix<'T> (cmd: MultiplyCommand) (cache: Map<string, MatrixInstance>) : MatrixInstance =
    let mx1 = cache.[cmd.LeftName]
    let mx2 = cache.[cmd.RightName]
    
    let m1 = FSharpMatrix.Matrix.convert<'T> mx1.Content
    let m2 = FSharpMatrix.Matrix.convert<'T> mx2.Content
    
    let mxRes = FSharpMatrix.Matrix.mulMatrix<'T> m1 m2
    let mxResStr = FSharpMatrix.Matrix.toStringMatrix mxRes
    
    {Content = mxResStr; _type = cmd.Type; size = mxResStr.Length}
    
    
let multiplyMatrix (cmd: MultiplyCommand) (cache: Map<string, MatrixInstance>) : MatrixInstance =
    match cmd.Type with
        | Int ->
            _innerMultiplyMatrix<int> cmd cache
        | Bool ->
            _innerMultiplyMatrix<bool> cmd cache
        | Double ->
            _innerMultiplyMatrix<double> cmd cache
        | Extended ->
            _innerMultiplyMatrix<double> cmd cache
        | _ -> failwith "Type error"
   
    
let findTrc (cmd: FindTrcCommand) (cache: Map<string, MatrixInstance>) : MatrixInstance =
        let mx = cache.[cmd.Name]
        let res = match mx._type with
            | Int ->
                mx.Content |> FSharpMatrix.Matrix.convert<int> |> FSharpMatrix.Matrix.transitiveClosure
            | Bool ->
                mx.Content |> FSharpMatrix.Matrix.convert<bool> |> FSharpMatrix.Matrix.transitiveClosure
            | Double ->
                mx.Content |> FSharpMatrix.Matrix.convert<double> |> FSharpMatrix.Matrix.transitiveClosure
            | Extended ->
                mx.Content |> FSharpMatrix.Matrix.convert<double> |> FSharpMatrix.Matrix.transitiveClosure
            | _ -> failwith "Type error"
         
        
        {Content = FSharpMatrix.Matrix.toStringMatrix(res); _type = Type.Bool; size = res.Length}