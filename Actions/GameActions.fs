namespace KnotsNCrosses.Actions

open System
open KnotsNCrosses.Models

module GameActions =
  let getNewSelectedGridPosition (oldSelectedGridPosition: int * int) (keyPress: ConsoleKeyInfo) =
    let (oldX, oldY) = oldSelectedGridPosition
    match keyPress.Key with
    | ConsoleKey.UpArrow -> (oldX, oldY - 1)
    | ConsoleKey.RightArrow -> (oldX + 1, oldY)
    | ConsoleKey.DownArrow -> (oldX, oldY + 1)
    | ConsoleKey.LeftArrow -> (oldX - 1, oldY)
    | _ -> (oldX, oldY)
  
  let isNewSelectedGridPositionOutOfBounds (newSelectedGridPosition: int * int) =
    let minX = 0
    let maxX = 2
    let minY = 0
    let maxY = 2
    let (newX, newY) = newSelectedGridPosition
    newX < minX || newX > maxX || newY < minY || newY > maxY
  
  let rec setNextGameState (oldBoard: Board) =
    ScreenActions.printBoard oldBoard
    let keyPress = (Console.ReadKey())
    let proposedNewSelectedGridPosition =
      getNewSelectedGridPosition
        (oldBoard.GetSelectedGridPosition())
        keyPress
    let newSelectedGridPosition =
      if isNewSelectedGridPositionOutOfBounds proposedNewSelectedGridPosition then
        oldBoard.GetSelectedGridPosition()
      else
        proposedNewSelectedGridPosition
    let playerAddMark = keyPress.Key = ConsoleKey.Spacebar
    setNextGameState (
      BoardActions.createBoard 
        (SquareActions.createNewSquareGridWhenPlayerAction
          newSelectedGridPosition
          playerAddMark 
          oldBoard)
        oldBoard.IsPlayerCross
    )
    

  let startGame () =
    let random = new Random()
    let isPlayerCross = random.Next(2) = 1
    let board = BoardActions.createBoardAtNewGame (0, 0) isPlayerCross
    setNextGameState board
