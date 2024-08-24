open System
open KnotsNCrosses.Models
open KnotsNCrosses.Actions

let startGame () =
  let board = new Board(SquareActions.createSquareGrid())
  ScreenActions.printBoard board
  Console.ReadLine() |> ignore 

[<EntryPoint>]
let main _ =
  startGame()
  0
