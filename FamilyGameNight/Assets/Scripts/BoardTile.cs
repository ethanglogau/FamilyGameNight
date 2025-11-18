using UnityEngine;

public class BoardTile : MonoBehaviour
{
    public enum TileColor { Black, White }
    public TileColor color;
    public Vector2Int boardPosition; // col, row
    public bool isOccupied;

    public void Initialize(TileColor pieceColor, Vector2Int position, bool occupied = false)
    {
        color = pieceColor;
        boardPosition = position;
        isOccupied = occupied;
        transform.position = new Vector3(position.x, position.y);
    }

    public void OnMouseDown()
    {
        GameManager.Instance.OnTileClicked(this);
    }
}
