using UnityEngine;

public class CheckerPiece : MonoBehaviour
{
    public enum PieceColor { Blue, Orange }
    public PieceColor color;
    public Vector2Int boardPosition; // row, col

    public void Initialize(PieceColor pieceColor, Vector2Int position)
    {
        color = pieceColor;
        boardPosition = position;
        transform.position = new Vector3(position.x, position.y);
    }
}
