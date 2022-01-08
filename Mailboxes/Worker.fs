module Mailboxes.Worker

open Mailboxes.Types
open Mailboxes.Actions
    
type Worker() =
    let errorEvent = Event<_>()
 
    let innerWorker =
        MailboxProcessor.Start(fun inbox ->
            
            let rec loop (cache: Map<string, MatrixInstance>) =
                async {
                        try
                            let! msg = inbox.Receive()
                            match msg with
                            | Create(cmd, reply) ->
                                let mx = createMatrix cmd
                                let newCache = cache.Add(cmd.Name, mx)
                                reply.Reply({Status = Status.Ok; matrixInstance = mx});
                                return! loop newCache
                            | Read(cmd, reply) ->
                                let mx = readMatrix cmd
                                let newCache = cache.Add(cmd.Name, mx)
                                reply.Reply({Status = Status.Ok; matrixInstance = mx});
                                return! loop newCache
                            | Write(cmd, reply) ->
                                let path = writeMatrix cmd cache
                                reply.Reply({Status = Status.Ok; Path = path});
                                return! loop cache
                            | Multiply(cmd, reply) ->
                                let resMx = multiplyMatrix cmd cache
                                reply.Reply({Status = Status.Ok; matrixInstance = resMx});
                                return! loop cache
                            | FindTrc(cmd, reply) ->
                                let resMx = findTrc cmd cache
                                reply.Reply({Status = Status.Ok; matrixInstance = resMx});
                                return! loop cache
                            | Exit(callback) ->
                                callback()
                                return ()
                            with e ->
                                match e with
                                | IntendedExit(e) -> printfn $"{e}"; return ()
                                | e -> errorEvent.Trigger(e); return! loop cache
                        }
            loop Map.empty<string, MatrixInstance>)
    
   
            
    member this.Create(cmd: CreateCommand) = innerWorker.PostAndReply((fun reply -> Create(cmd, reply)), timeout = 2000)
    member this.Read(cmd: ReadCommand) = innerWorker.PostAndReply((fun reply -> Read(cmd, reply)), timeout = 2000)
    member this.Write(cmd: WriteCommand) = innerWorker.PostAndReply((fun reply -> Write(cmd, reply)), timeout = 2000)
    member this.Multiply(cmd: MultiplyCommand) = innerWorker.PostAndReply((fun reply -> Multiply(cmd, reply)), timeout = 2000)
    member this.FindTrc(cmd: FindTrcCommand) = innerWorker.PostAndReply((fun reply -> FindTrc(cmd, reply)), timeout = 2000)
    member this.Exit(callback: unit->unit) = innerWorker.Post(Exit(callback))
    member this.Error(callback) = innerWorker.Error.Add(callback)
    member this.OnError = errorEvent.Publish