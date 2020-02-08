// Learn more about F# at http://fsharp.org

open System
open GameEngine

[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"

    GameEngine.Run 5 
    0 // return an integer exit code
