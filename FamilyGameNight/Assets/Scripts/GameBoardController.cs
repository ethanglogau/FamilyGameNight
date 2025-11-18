using System.Drawing;
using UnityEngine;

public class GameBoardController : MonoBehaviour
{
    public static GameBoardController instance;

    public GameObject whiteSquare;
    public GameObject blackSquare;
    public int boardSize = 8;
    public float squareSize = 1.0f;

    public GameObject bluePiece;
    public GameObject orangePiece;

    public BoardTile[,] tiles;
    public CheckerPiece[,] pieces;

    private void Awake()
    {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create the game board
        GenerateBoard();
        SetupPieces();
    }

    public BoardTile GetBoardTile(Vector2Int pos)
    {
        return tiles[pos.x, pos.y] ?? null;
    }

    public CheckerPiece GetCheckerPiece(Vector2Int pos)
    {
        return pieces[pos.x, pos.y];
    }

    void GenerateBoard()
    {
        tiles = new BoardTile[boardSize,boardSize];

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                // Determine color based on checkerboard pattern
                bool isBlack = (row + col) % 2 == 0;
                GameObject prefabToUse = isBlack ? blackSquare : whiteSquare;

                SpawnTile(prefabToUse, isBlack ? BoardTile.TileColor.Black : BoardTile.TileColor.White, row, col);
                //// Calculate position
                //Vector3 position = new Vector3(row * squareSize, col * squareSize);

                //// Instantiate square
                //GameObject square = Instantiate(prefabToUse, position, Quaternion.identity);
                //square.transform.parent = this.transform; // Optional: keep hierarchy clean
            }
        }
    }

    void SetupPieces()
    {
        pieces = new CheckerPiece[boardSize, boardSize];

        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                if ((row + col) % 2 != 0) // only on black squares
                {
                    if (row < 3)
                    {
                        SpawnPiece(bluePiece, CheckerPiece.PieceColor.Blue, row, col);
                        tiles[col, row].isOccupied = true;
                    }
                    else if (row > 4)
                    {
                        SpawnPiece(orangePiece, CheckerPiece.PieceColor.Orange, row, col);
                        tiles[col, row].isOccupied = true;
                    }
                }
            }
        }
    }

    void SpawnPiece(GameObject prefab, CheckerPiece.PieceColor color, int row, int col)
    {
        GameObject pieceObj = Instantiate(prefab);
        CheckerPiece piece = pieceObj.GetComponent<CheckerPiece>();
        piece.Initialize(color, new Vector2Int(col, row));
        pieceObj.transform.parent = this.transform;
        pieces[col, row] = piece;
    }

    void SpawnTile(GameObject prefab, BoardTile.TileColor color, int row, int col)
    {
        GameObject tileObj = Instantiate(prefab);
        BoardTile tile = tileObj.GetComponent<BoardTile>();
        tile.Initialize(color, new Vector2Int(col, row));
        tileObj.transform.parent = this.transform;
        tiles[col, row] = tile;
    }


}
