namespace KnotsNCrosses.Models

type IMark =
  abstract member Positions: (int * int) list with get

type Space() =
  interface IMark with
    member _.Positions
      with get () = []

type Cross(squareStartingPosition: (int * int)) =
  let (x, y) = squareStartingPosition
  interface IMark with
    member _.Positions
      with get() = [
        (x + 2, y + 2); (x + 4, y + 2);
        (x + 3, y + 3);
        (x + 2, y + 4); (x + 4, y + 4)]

type Knot(squareStartingPosition: (int * int)) =
  let (x, y) = squareStartingPosition
  interface IMark with
    member _.Positions
      with get() = [
        (x + 2, y + 2); (x + 3, y + 2); (x + 4, y + 2);
        (x + 2, y + 3); (x + 4, y + 3);
        (x + 2, y + 4); (x + 3, y + 4); (x + 4, y + 4)]

type Square(consoleStartingPosition: (int * int), size: int, selected: bool, mark: IMark) =
  let (startingX, startingY) = consoleStartingPosition
  let squareWidthSpan = [startingX..startingX + size]
  let squareHeightSpan = [startingY..startingY + size]
  let topWall =
    squareWidthSpan |> List.map(fun x -> (x, startingY))
  let rightWall =
    squareHeightSpan |> List.map(fun y -> (startingX + size, y))
  let bottomWall =
    squareWidthSpan |> List.map(fun x -> (x, startingY + size))
  let leftWall =
    squareHeightSpan |> List.map(fun y -> (startingX, y))
  let positions =
    topWall
    |> List.append rightWall
    |> List.append leftWall
    |> List.append bottomWall

  member _.ConsoleStartingPosition
    with get() = consoleStartingPosition
  
  member _.Size
    with get() = size

  member _.Positions
    with get() = positions
  
  member _.Selected
    with get() = selected

  member _.Mark
    with get() = mark

