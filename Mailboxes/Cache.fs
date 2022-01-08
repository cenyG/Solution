module Mailboxes.Cache

open System.Collections.Generic
open Mailboxes.Types


type Cache() =
    let cache = Map.empty<string, MatrixInstance>
    member this.Set (key: string) (value: MatrixInstance) =
        cache.Add(key, value)
    member this.Get (key: string) =
        Some cache.[key]