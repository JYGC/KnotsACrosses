namespace KnotsNCrosses.Actions

open KnotsNCrosses.Models

module BoardActions =
  let createBoardAtNewGame (selectedGrid: (int * int)) (isPlayerCross: bool) =
    new Board(
      SquareActions.createSquareGrid selectedGrid,
      isPlayerCross
    )
  
  let createBoard (isPlayerCross: bool) (squares: Map<(int * int), Square>) =
    new Board(
      squares,
      isPlayerCross
    )
  
  let getSelectedGridPosition (board: Board) =
    board.Squares
    |> Map.toList
    |> List.find(fun (_, s) -> s.Selected)
    |> fst
