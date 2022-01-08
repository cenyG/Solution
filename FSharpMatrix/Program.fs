open System


[<EntryPoint>]
let main argv =
    try
        printf "hi mom" 
        0

    with e -> 
        printfn "%A" e
        -1
