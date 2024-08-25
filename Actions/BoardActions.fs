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