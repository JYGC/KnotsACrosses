namespace KnotsNCrosses.Models

type Board(squares: Square list list, isPlayerCross: bool) =
  member _.Squares
    with get() = squares
  
  member _.IsPlayerCross
    with get() = isPlayerCross

  member _.GetSelectedGridPosition() =
    squares
    |> List.indexed
    |> List.map(fun (gridY, row) ->
      row
      |> List.indexed
      |> List.map(fun (gridX, s) -> ((gridX, gridY), s.Selected)))
    |> List.reduce List.append
    |> List.find(fun (_, selected) -> selected)
    |> fst
