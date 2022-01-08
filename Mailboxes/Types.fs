module Mailboxes.Types

open System

type Type =
    | Int
    | Bool
    | Double
    | Extended
    
type Status =
    | Ok
    | Err of string

type MatrixInstance = {Content: string[,]; _type: Type; size: int}

type CreateCommand = {Name: string; Type: Type; Size: int}
type ReadCommand = {Name: string; Type: Type; FileName: string; Path: string}
type WriteCommand = {Name: string; Path: string}
type MultiplyCommand = {LeftName: string; RightName: string; Type: Type}
type FindTrcCommand = {Name: string}
type ExitCommand = unit -> unit

type CreateResponse = {Status: Status; matrixInstance: MatrixInstance}
type ReadResponse = {Status: Status; matrixInstance: MatrixInstance}
type WriteResponse = {Status: Status; Path: string}
type MultiplyResponse = {Status: Status; matrixInstance: MatrixInstance}
type FindTrcResponse = {Status: Status; matrixInstance: MatrixInstance}


type Command =
    | Create of CreateCommand * AsyncReplyChannel<CreateResponse>
    | Read of ReadCommand * AsyncReplyChannel<ReadResponse>
    | Write of WriteCommand * AsyncReplyChannel<WriteResponse>
    | Multiply of MultiplyCommand * AsyncReplyChannel<MultiplyResponse>
    | FindTrc of FindTrcCommand * AsyncReplyChannel<FindTrcResponse>
    | Exit of ExitCommand

exception IntendedExit of string

let getType _type =
    match _type with
    | "int" -> Int
    | "bool" -> Bool
    | "double" -> Double
    | "extended" -> Extended
    | _ -> raise (Exception("Wrong type specify"))