open System

let drawHollowSquareLine size color x y isBorder =
  Console.SetCursorPosition(x, y)
  if isBorder then
    Console.BackgroundColor <- color
    String.replicate size " " |> Console.Write
  else
    Console.BackgroundColor <- color
    Console.Write " "
    Console.ResetColor ()
    String.replicate (size - 2) " " |> Console.Write
    Console.BackgroundColor <- color
    Console.Write " "
  Console.ResetColor ()

let drawHollowSquare size color x y =
  [0..(size-1)]
  |> List.iter (fun i -> 
    drawHollowSquareLine size color x (y + i) (i = 0 || i = size - 1))

let drawSquareAtPosition size color x y =
  drawHollowSquare size color x y

let drawRowOfSquares size color separation count startX startY =
  [0..(count-1)] |> List.iter (fun i ->
    drawSquareAtPosition size color (startX + i * (size + separation)) startY)

let drawGridOfSquares size color separation count rowSeparation startX startY =
  [0..(count-1)] |> List.iter (fun i ->
    drawRowOfSquares size color separation count startX (startY + i * (size + rowSeparation)))

[<EntryPoint>]
let main _ =
  drawGridOfSquares 5 ConsoleColor.Red 1 3 1 0 0
  Console.SetCursorPosition(0, (5 * 3) + (1 * 2) + 1)  // Reset cursor position after drawing
  0
