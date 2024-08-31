namespace KnotsNCrosses.Actions

open KnotsNCrosses.Models

module SquareActions =
  let private getConsoleXY (gridPosition: (int * int)) (squareSize: int) =
    let (gridX, gridY) = gridPosition
    (gridX * (squareSize + 2), gridY * (squareSize + 2))

  let createSquareGrid (selectedGridPosition: (int * int)) =
    let gridWidth = 2
    let gridHeight = 2
    let squareSize = 6
    Map [
      for row in 0..gridHeight do
        for cell in 0..gridWidth do
          let gridPosition = (cell, row)
          let consoleStartingPosition = getConsoleXY gridPosition squareSize
          (
            gridPosition,
            new Square(
              consoleStartingPosition = consoleStartingPosition,
              size = squareSize,
              selected = (gridPosition = selectedGridPosition),
              mark = new Space()
            )
          )
    ]

  let private getSquareMark (isNewSelectedGridPosition: bool) (playerAddMark: bool) (oldSquare: Square) (board: Board): IMark =
    if isNewSelectedGridPosition && playerAddMark then
      if board.IsPlayerCross then
        new Cross(oldSquare.ConsoleStartingPosition)
      else
        new Knot(oldSquare.ConsoleStartingPosition)
    else
      oldSquare.Mark

  let createNewSquareGridWhenPlayerAction (newSelectedGridPosition: (int * int)) (playerAddMark: bool) (board: Board) =
    Map [
      for gs in board.Squares do
        let gridPosition = gs.Key
        let oldSquare = gs.Value
        let isNewSelectedGridPosition = gridPosition = newSelectedGridPosition
        let newSquare = new Square(
          consoleStartingPosition = oldSquare.ConsoleStartingPosition,
          size = oldSquare.Size,
          selected = isNewSelectedGridPosition,
          mark = getSquareMark isNewSelectedGridPosition playerAddMark oldSquare board
        )
        (gridPosition, newSquare)
    ]