module FSharpMatrix.Generic

let Add<'T> (a: 'T) (b: 'T) : 'T=
    let t = typeof<'T>
    match t with
    | t when t = typeof<int32> -> unbox(((box a) :?> int) + ((box b) :?> int))
    | t when t = typeof<double> -> unbox(((box a) :?> double) + ((box b) :?> double))
    | t when t = typeof<bool> -> unbox(((box a) :?> bool) || ((box b) :?> bool))
    | _ -> failwith "Type error"


let Mul<'T> (a: 'T) (b: 'T) : 'T=
    let t = typeof<'T>
    match t with
    | t when t = typeof<int32> -> unbox(((box a) :?> int) * ((box b) :?> int))
    | t when t = typeof<double> -> unbox(((box a) :?> double) * ((box b) :?> double))
    | t when t = typeof<bool> -> unbox(((box a) :?> bool) && ((box b) :?> bool))
    | _ -> failwith "Type error"


let IsNil<'T> (a: 'T) (b: 'T) : bool=
    let t = typeof<'T>
    match t with
    | t when t = typeof<int32> -> ((box a) :?> int) = 0
    | t when t = typeof<double> -> ((box a) :?> double) = 0.0
    | t when t = typeof<bool> -> ((box a) :?> bool) = false
    | _ -> failwith "Type error"


let ToDouble<'T> (a: 'T) : double=
    let t = typeof<'T>
    match t with
    | t when t = typeof<int32> -> System.Convert.ToDouble((box a) :?> int)
    | t when t = typeof<double> -> System.Convert.ToDouble((box a) :?> double)
    | t when t = typeof<bool> -> System.Convert.ToDouble((box a) :?> bool)
    | _ -> failwith $"Can't convert {t} to double"
    
    
let Convert<'T> (a: string): 'T =
    let t = typeof<'T>
    match t with
    | t when t = typeof<int32> -> unbox(System.Int32.Parse(a))
    | t when t = typeof<double> -> unbox(System.Double.Parse(a))
    | t when t = typeof<bool> -> unbox(System.Boolean.Parse(a))
    | _ -> failwith $"Can't convert {t}"