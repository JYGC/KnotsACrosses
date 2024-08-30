namespace KnotsNCrosses.Models

type Board(squares: Square list list, isPlayerCross: bool) =
  member _.Squares
    with get() = squares
  
  member _.IsPlayerCross
    with get() = isPlayerCross
