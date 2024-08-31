namespace KnotsNCrosses.Models

type Board(squares: Map<(int * int), Square>, isPlayerCross: bool) =
  member _.Squares
    with get() = squares
  
  member _.IsPlayerCross
    with get() = isPlayerCross
