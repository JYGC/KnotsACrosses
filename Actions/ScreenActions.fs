namespace KnotsNCrosses.Actions

open System
open KnotsNCrosses.Models

module ScreenActions =
  let printSqaure (square: Square) =
    Console.BackgroundColor <- ConsoleColor.DarkGray
    square.Positions
    |> List.iter(fun p ->
      Console.SetCursorPosition p
      Console.Write " ")
    Console.ResetColor()
    Console.BackgroundColor <- ConsoleColor.DarkGreen
    square.Mark.Positions
    |> List.iter(fun mp ->
      Console.SetCursorPosition mp
      Console.Write " ")
    Console.ResetColor()

  let printBoard (board: Board) =
    Console.Clear()
    board.Squares
    |> List.iter(fun r ->
      r |> List.iter printSqaure)