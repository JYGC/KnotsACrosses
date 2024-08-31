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
    let oldSelectedGridPosition = BoardActions.getSelectedGridPosition oldBoard
    let proposedNewSelectedGridPosition =
      getNewSelectedGridPosition
        oldSelectedGridPosition
        keyPress
    let newSelectedGridPosition =
      if isNewSelectedGridPositionOutOfBounds proposedNewSelectedGridPosition then
        oldSelectedGridPosition
      else
        proposedNewSelectedGridPosition
    let playerAddMark = keyPress.Key = ConsoleKey.Spacebar
    let newSquares =
      SquareActions.createNewSquareGridWhenPlayerAction
        newSelectedGridPosition
        playerAddMark 
        oldBoard
    let newBoard =
      BoardActions.createBoard 
        newSquares
        oldBoard.IsPlayerCross
    setNextGameState newBoard

    

  let startGame () =
    let random = new Random()
    let isPlayerCross = random.Next(2) = 1
    let board = BoardActions.createBoardAtNewGame (0, 0) isPlayerCross
    setNextGameState board
