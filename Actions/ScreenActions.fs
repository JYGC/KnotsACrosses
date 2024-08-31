namespace KnotsNCrosses.Actions

open System
open KnotsNCrosses.Models

module ScreenActions =
  let private selectSqaureColor (square: Square) =
    if square.Selected then
      ConsoleColor.Green
    else
      ConsoleColor.DarkGray

  let private printSqaure (square: Square) =
    Console.BackgroundColor <- selectSqaureColor square
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
    |> Map.toList
    |> List.iter(fun (p, s) ->
      printSqaure s
    )