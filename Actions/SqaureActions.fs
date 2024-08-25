namespace KnotsNCrosses.Actions

open KnotsNCrosses.Models

module SquareActions =
  let private getConsoleY (gridY: int) (squareSize: int) = gridY * (squareSize + 2)
  let private getConsoleX (gridX: int) (squareSize: int) = gridX * (squareSize + 2)

  let private createSquareRow (squareSize: int) (gridY: int) (selectedGridPosition: (int * int)) =
    let consoleY = getConsoleY gridY squareSize
    [0..2]
    |> List.map(fun x -> 
      let startingPosition = (getConsoleX x squareSize, consoleY)
      new Square(
        startingPosition = startingPosition,
        size = squareSize,
        selected = ((x, consoleY) = selectedGridPosition),
        mark = new Space()
      ))

  let createSquareGrid (selectedGridPosition: (int * int)) =
    let squareSize = 6
    [0..2]
    |> List.map(fun y -> createSquareRow squareSize y selectedGridPosition)

  let getSquareMark (isNewSelectedGridPosition: bool) (playerAddMark: bool) (oldSquare: Square) (board: Board): IMark =
    if isNewSelectedGridPosition && playerAddMark then
      if board.IsPlayerCross then
        new Cross(oldSquare.StartingPosition)
      else
        new Knot(oldSquare.StartingPosition)
    else
      oldSquare.Mark

  let createNewSquareGridWhenPlayerAction (newSelectedGridPosition: (int * int)) (playerAddMark: bool) (board: Board) =
    board.Squares
    |> List.indexed
    |> List.map(fun (gridY, row) ->
      row
      |> List.indexed
      |> List.map(fun (gridX, oldSqaure) ->
        let isNewSelectedGridPosition = (gridX, gridY) = newSelectedGridPosition
        new Square(
        startingPosition = oldSqaure.StartingPosition,
        size = oldSqaure.Size,
        selected = isNewSelectedGridPosition,
        mark = getSquareMark isNewSelectedGridPosition playerAddMark oldSqaure board
      )
    ))