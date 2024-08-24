namespace KnotsNCrosses.Actions

open KnotsNCrosses.Models

module SquareActions =
  let private createSquareRow (squareSize: int) (columnY: int) =
    [0..2]
    |> List.map(fun x -> 
      let startingPosition = (x * (squareSize + 2), columnY)
      new Square(
        startingPosition = startingPosition,
        size = squareSize,
        selected = false,
        mark = new Space()
      ))

  let createSquareGrid() =
    let squareSize = 6
    [0..2]
    |> List.map(fun y -> createSquareRow squareSize (y *  (squareSize + 2)))
    