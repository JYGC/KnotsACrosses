namespace KnotsNCrosses.Actions

open KnotsNCrosses.Models

module BoardActions =
  let createBoardAtNewGame (selectedGrid: (int * int)) (isPlayerCross: bool) =
    new Board(
      SquareActions.createSquareGrid selectedGrid,
      isPlayerCross
    )
  
  let createBoard (squares: Square list list) (isPlayerCross: bool) =
    new Board(
      squares,
      isPlayerCross
    )
  
  let getSelectedGridPosition (board: Board) =
    board.Squares
    |> List.indexed
    |> List.map(fun (gridY, row) ->
      row
      |> List.indexed
      |> List.map(fun (gridX, s) -> ((gridX, gridY), s.Selected)))
    |> List.reduce List.append
    |> List.find(fun (_, selected) -> selected)
    |> fst