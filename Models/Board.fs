namespace KnotsNCrosses.Models

type Board(squares: Map<(int * int), Square>, isPlayerCrossTurn: bool) =
  member _.Squares
    with get() = squares
  
  member _.IsPlayerCrossTurn
    with get() = isPlayerCrossTurn
