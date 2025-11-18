using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameBoardController boardController;

    private GameState currentState = GameState.WaitingForSelection;
    private CheckerPiece selectedPiece;
    private CheckerPiece.PieceColor activeColor = CheckerPiece.PieceColor.Blue;


    void Awake()
    {
        Instance = this;
    }

    public void OnPieceClicked(CheckerPiece piece)
    {
        if (currentState == GameState.WaitingForSelection && piece.color == activeColor)
        {
            selectedPiece = piece;

            // Draw possible moves to board
            selectedPiece.validMoves = GetValidMoves();

            currentState = GameState.WaitingForMove;
            Debug.Log("Player " + activeColor + " selected " + piece.name);
        }
    }

    public void OnTileClicked(BoardTile tile)
    {
        if (currentState == GameState.WaitingForMove && selectedPiece != null && selectedPiece.validMoves.Contains(tile))
        {
            selectedPiece.MoveTo(tile);
            selectedPiece = null;
            currentState = GameState.TurnEnd;
            EndTurn();
        }
    }

    private void EndTurn()
    {
        // Check win condition first
        if (CheckWinCondition())
        {
            currentState = GameState.GameOver;
            Debug.Log("Game Over!");
            return;
        }

        // Swap player
        activeColor = (activeColor == CheckerPiece.PieceColor.Blue) ? CheckerPiece.PieceColor.Orange : CheckerPiece.PieceColor.Blue;
        currentState = GameState.WaitingForSelection;
        Debug.Log("Now it's Player " + activeColor + "'s turn.");
    }

    private bool CheckWinCondition()
    {
        // Example: check if opponent has no pieces left
        //bool opponentHasPieces = Board.Instance.HasPiecesForPlayer(activePlayer == 1 ? 2 : 1);
        return false;
    }

    private List<BoardTile> GetValidMoves()
    {
        List<BoardTile> validMoves = new List<BoardTile>();

        int direction = (selectedPiece.color == CheckerPiece.PieceColor.Blue) ? 1 : -1;

        // Directions to check
        List<Vector2Int> directions = new List<Vector2Int>();
        directions.Add(new Vector2Int(1, direction));
        directions.Add(new Vector2Int(-1, direction));
        if (selectedPiece.isKinged)
        {
            directions.Add(new Vector2Int(1, -direction));
            directions.Add(new Vector2Int(-1, -direction));
        }

        foreach (var dir in directions)
        {
            Vector2Int target = selectedPiece.boardPosition + dir;

            if (IsInsideBoard(target))
            {
                BoardTile targetTile = boardController.GetBoardTile(target);

                // Simple move
                if (!targetTile.isOccupied)
                {
                    validMoves.Add(targetTile);
                }
                // Capture
                else if (targetTile.isOccupied && boardController.GetCheckerPiece(target) != null)
                {
                    Vector2Int landing = selectedPiece.boardPosition + dir * 2;
                    if (IsInsideBoard(landing))
                    {
                        BoardTile landingTile = boardController.GetBoardTile(landing);
                        if (landingTile.isOccupied)
                        {
                            validMoves.Add(landingTile);
                            // Recursive check for multiple jumps could go here
                        }
                    }
                }
            }
        }

        return validMoves;
    }

    private bool IsInsideBoard(Vector2Int pos)
    {
        if (pos.y >= 0 && pos.y <= 7 && pos.x >= 0 && pos.x <= 7)
        {
            return true;
        }
        return false;
    }

}

public enum GameState
{
    WaitingForSelection,
    WaitingForMove,
    TurnEnd,
    GameOver
}
