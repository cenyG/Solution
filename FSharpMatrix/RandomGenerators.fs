module FSharpMatrix.RandomGenerators

open System

let private rnd = Random()

let toPrecisions value precisions = Math.Round(value * Math.Pow(10.0, precisions)) / Math.Pow(10.0, precisions)

let randomBool = fun (max: bool) -> if max = false then false else rnd.Next(0,2) = 1

let randomInt (max: int) =
    rnd.Next(0, max)

let randomDouble (max: double) =
    let _rnd = rnd.NextDouble() * max
    toPrecisions _rnd 2.0

let randomExtendedDouble (max: double) =
    let _rnd = rnd.NextDouble() * max
    if _rnd > 0.85 * max then infinity
    else toPrecisions _rnd 2.0
