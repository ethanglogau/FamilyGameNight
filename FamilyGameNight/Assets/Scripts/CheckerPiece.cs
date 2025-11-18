using System.Collections.Generic;
using UnityEngine;

public class CheckerPiece : MonoBehaviour
{
    public enum PieceColor { Blue, Orange }
    public PieceColor color;
    public Vector2Int boardPosition; // col, row
    public List<BoardTile> validMoves;
    public bool isKinged;

    public void Initialize(PieceColor pieceColor, Vector2Int position)
    {
        color = pieceColor;
        boardPosition = position;
        validMoves = new List<BoardTile>();
        isKinged = false;
        transform.position = new Vector3(position.x, position.y);
    }

    public void MoveTo(BoardTile tile)
    {
        transform.position = tile.transform.position;
    }

    public void OnMouseDown()
    {
        GameManager.Instance.OnPieceClicked(this);
    }
}
