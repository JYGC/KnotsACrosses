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
  
  let getResultsOfPlayerAction (keyPress: ConsoleKeyInfo) (oldBoard: Board) =
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
    (playerAddMark, newSelectedGridPosition)

  let private hasPlayerNeetFormation (oldBoard: Board) (gridFormation: (int * int) list) =
      gridFormation
      |> List.map(fun p ->
        oldBoard.Squares[p].Mark
      )
      |> List.forall(fun m ->
        if oldBoard.IsPlayerCross then
          m :? Cross
        else
          m :? Knot
      )


  let hasPlayerWon (oldBoard: Board) =
    let winningFormations =
      [
        [(0, 0); (1, 0); (2, 0)]; [(0, 1); (1, 1); (2, 1)]; [(0, 2); (1, 2); (2, 2)];
        [(0, 0); (0, 1); (0, 2)]; [(1, 0); (1, 1); (1, 2)]; [(2, 0); (2, 1); (2, 2)];
        [(0, 0); (1, 1); (2, 2)]; [(0, 2); (1, 1); (2, 0)]
      ]
    winningFormations
    |> List.exists(fun formation ->
      hasPlayerNeetFormation oldBoard formation = true
    )
  
  let rec setNextGameState (oldBoard: Board) =
    ScreenActions.printBoard oldBoard
    if hasPlayerWon oldBoard then
      let playerName =
        if oldBoard.IsPlayerCross then
          "Cross"
        else
          "Knot"
      Console.WriteLine $"Player {playerName} won."
    else
      let keyPress = (Console.ReadKey())
      let (playerAddMark, newSelectedGridPosition) =
        getResultsOfPlayerAction keyPress oldBoard
      oldBoard
      |> SquareActions.createNewSquareGridWhenPlayerAction
        newSelectedGridPosition
        playerAddMark
      |> BoardActions.createBoard oldBoard.IsPlayerCross
      |> setNextGameState

  let startGame () =
    let random = new Random()
    let isPlayerCross = random.Next(2) = 1
    let board = BoardActions.createBoardAtNewGame (0, 0) isPlayerCross
    setNextGameState board
