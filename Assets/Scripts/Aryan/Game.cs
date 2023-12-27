using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public GameObject chesspiecePrefab;

    private GameObject[] positions = new GameObject[64];

    private int moveNumber = 1;
    private GameObject selectedPiece;
    void Start()
    {
        // White pieces
        positions[0 + 0 * 8] = CreatePiece("white", "rook", 0, 0);
        positions[1 + 0 * 8] = CreatePiece("white", "knight", 1, 0);
        positions[2 + 0 * 8] = CreatePiece("white", "bishop", 2, 0);
        positions[3 + 0 * 8] = CreatePiece("white", "queen", 3, 0);
        positions[4 + 0 * 8] = CreatePiece("white", "king", 4, 0);
        positions[5 + 0 * 8] = CreatePiece("white", "bishop", 5, 0);
        positions[6 + 0 * 8] = CreatePiece("white", "knight", 6, 0);
        positions[7 + 0 * 8] = CreatePiece("white", "rook", 7, 0);
        positions[0 + 1 * 8] = CreatePiece("white", "pawn", 0, 1);
        positions[1 + 1 * 8] = CreatePiece("white", "pawn", 1, 1);
        positions[2 + 1 * 8] = CreatePiece("white", "pawn", 2, 1);
        positions[3 + 1 * 8] = CreatePiece("white", "pawn", 3, 1);
        positions[4 + 1 * 8] = CreatePiece("white", "pawn", 4, 1);
        positions[5 + 1 * 8] = CreatePiece("white", "pawn", 5, 1);
        positions[6 + 1 * 8] = CreatePiece("white", "pawn", 6, 1);
        positions[7 + 1 * 8] = CreatePiece("white", "pawn", 7, 1);

        // Black pieces
        positions[0 + 6 * 8] = CreatePiece("black", "pawn", 0, 6);
        positions[1 + 6 * 8] = CreatePiece("black", "pawn", 1, 6);
        positions[2 + 6 * 8] = CreatePiece("black", "pawn", 2, 6);
        positions[3 + 6 * 8] = CreatePiece("black", "pawn", 3, 6);
        positions[4 + 6 * 8] = CreatePiece("black", "pawn", 4, 6);
        positions[5 + 6 * 8] = CreatePiece("black", "pawn", 5, 6);
        positions[6 + 6 * 8] = CreatePiece("black", "pawn", 6, 6);
        positions[7 + 6 * 8] = CreatePiece("black", "pawn", 7, 6);
        positions[0 + 7 * 8] = CreatePiece("black", "rook", 0, 7);
        positions[1 + 7 * 8] = CreatePiece("black", "knight", 1, 7);
        positions[2 + 7 * 8] = CreatePiece("black", "bishop", 2, 7);
        positions[3 + 7 * 8] = CreatePiece("black", "queen", 3, 7);
        positions[4 + 7 * 8] = CreatePiece("black", "king", 4, 7);
        positions[5 + 7 * 8] = CreatePiece("black", "bishop", 5, 7);
        positions[6 + 7 * 8] = CreatePiece("black", "knight", 6, 7);
        positions[7 + 7 * 8] = CreatePiece("black", "rook", 7, 7);

    }

    public void Update()
    {
        // Make object follow mouse
        if (selectedPiece)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = -1;
            selectedPiece.transform.position = Vector3.Lerp(selectedPiece.transform.position, mousePosition, 1.0f);
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!selectedPiece)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if (hit.collider && hit.collider.tag == "Chesspiece")
                {
                    selectedPiece = hit.collider.gameObject;
                }
            }
        }

        // Drop piece into another position
        if (Input.GetMouseButtonUp(0))
        {
            if (selectedPiece)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                BoardPosition position = Utility.CoordinatesToBoardPosition(mousePosition);
                selectedPiece.GetComponent<Chesspiece>().Move(position.x, position.y);
                selectedPiece = null;
            }
        }
    }

    public GameObject CreatePiece(string player, string piece, int x, int y)
    {
        GameObject obj = Instantiate(chesspiecePrefab, new Vector3(0, 0, 0), Quaternion.identity);

        Chesspiece chesspiece = obj.GetComponent<Chesspiece>();

        chesspiece.Initialize(player, piece, x, y);
        return obj;
    }

    public bool PositionIsEmpty(int x, int y)
    {
        return positions[x + y * 8] == null;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x + y * 8] = null;
    }

    public void SetPosition(GameObject piece)
    {
        BoardPosition position = piece.GetComponent<Chesspiece>().GetPosition();

        positions[position.x + position.y * 8] = piece;
    }

    public GameObject GetPosition(int x, int y)
    {
        return positions[x + y * 8];
    }
    public int GetMoveNumber()
    {
        return moveNumber;
    }
}
