module FSharpTests

open FSharpMatrix
open NUnit.Framework


let matrices1 = Seq.init 1000 (fun a -> Matrix.generateMatrix 8 8 1000 RandomGenerators.randomInt) |> Seq.toArray
let matrices2 = Seq.init 1000 (fun a -> Matrix.generateMatrix 8 8 1000 RandomGenerators.randomInt) |> Seq.toArray

let log_m m1 m2 m3 =
    printfn "matrix A:"
    Matrix.printMatrix(m1)
    printfn "matrix B:"
    Matrix.printMatrix(m2)
    printfn "matrix Result:"
    Matrix.printMatrix(m3)

[<TestFixture>]
type FSharpTests() = 
   
    [<Test>]     
    member this.TestParallelMul () = 
        for i in 1..matrices1.Length - 1 do
            let res = Matrix.mulParallel matrices1.[i] matrices2.[i]
            log_m matrices1.[i] matrices2.[i] res
            
    [<Test>]
    member this.TestMul () = 
        for i in 1..matrices1.Length - 1 do
            let res = Matrix.mulMatrix matrices1.[i] matrices2.[i]
            log_m matrices1.[i] matrices2.[i] res