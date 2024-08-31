namespace KnotsNCrosses.Actions

open System
open KnotsNCrosses.Models

module ScreenActions =
  let private selectPlayerSelectedColor (isPlayerCrossTurn: bool) = 
      if isPlayerCrossTurn then
        ConsoleColor.Green
      else
        ConsoleColor.Magenta
  let private selectSqaureColor (square: Square) (board: Board) =
    if square.Selected then
      selectPlayerSelectedColor board.IsPlayerCrossTurn
    else
      ConsoleColor.DarkGray
  
  let private selectMarkColor (mark: IMark) =
    if mark :? Cross then
      ConsoleColor.DarkGreen
    else
      ConsoleColor.DarkMagenta

  let private printSqaure (square: Square) (board: Board) =
    Console.BackgroundColor <- selectSqaureColor square board
    square.Positions
    |> List.iter(fun p ->
      Console.SetCursorPosition p
      Console.Write " ")
    Console.ResetColor()
    Console.BackgroundColor <- selectMarkColor square.Mark
    square.Mark.Positions
    |> List.iter(fun mp ->
      Console.SetCursorPosition mp
      Console.Write " ")
    Console.ResetColor()

  let printBoard (board: Board) =
    Console.Clear()
    board.Squares
    |> Map.toList
    |> List.iter(fun (_, s) ->
      printSqaure s board
    )
  
  let printWinningMessage (playerType: Type) =
    Console.ForegroundColor <-
      selectPlayerSelectedColor (playerType = typeof<Cross>)
    Console.WriteLine $"Player {playerType.Name} is won. Game over."
    Console.ResetColor()
  
  let printGameDrawMessage () =
    Console.WriteLine "All squares filled but not player won. Game over."
