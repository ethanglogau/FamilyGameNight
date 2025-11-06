using UnityEngine;

public class GameBoardController : MonoBehaviour
{
    public GameObject whiteSquare;
    public GameObject blackSquare;
    public int boardSize = 8;
    public float squareSize = 1.0f;

    public GameObject bluePiece;
    public GameObject orangePiece;

    private CheckerPiece[,] pieces;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Create the game board
        GenerateBoard();
        SetupPieces();
    }

    void GenerateBoard()
    {
        for (int row = 0; row < boardSize; row++)
        {
            for (int col = 0; col < boardSize; col++)
            {
                // Determine color based on checkerboard pattern
                bool isBlack = (row + col) % 2 == 0;
                GameObject prefabToUse = isBlack ? blackSquare : whiteSquare;

                // Calculate position
                Vector3 position = new Vector3(row * squareSize, col * squareSize);

                // Instantiate square
                GameObject square = Instantiate(prefabToUse, position, Quaternion.identity);
                square.transform.parent = this.transform; // Optional: keep hierarchy clean
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
                    }
                    else if (row > 4)
                    {
                        SpawnPiece(orangePiece, CheckerPiece.PieceColor.Orange, row, col);
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
        pieces[row, col] = piece;
    }


}
