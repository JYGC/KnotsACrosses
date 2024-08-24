namespace KnotsNCrosses.Models

type Board(squares: Square list list) =
  member _.Squares
    with get() = squares

