using System;
using System.Collections.Generic;
using UnityEngine;

public class Chesspiece : MonoBehaviour
{
    private Game controller;

    // Board Position
    private int x = -1;
    private int y = -1;

    // Player (Black or White)
    private string player;

    // Type of piece
    private string piece;

    private int timesMoved = 0;

    private Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    public Sprite[] sprites;

    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<Game>();

        foreach (var sprite in sprites)
        {
            spriteDictionary.Add(sprite.name, sprite);
        }
    }

    public void Initialize(string player, string piece, int x, int y)
    {
        this.player = player;
        this.piece = piece;
        this.x = x;
        this.y = y;

        spriteRenderer.sprite = spriteDictionary[string.Format("{0}_{1}", player, piece)];

        SetPosition(x, y);
    }

    public bool CanMovePosition(int x, int y)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8 && (x != this.x || y != this.y))
        {
            return piece switch
            {
                "pawn" => PawnMovement(x, y),
                "bishop" => DiagonalMovement(x, y),
                "knight" => KnightMovement(x, y),
                "queen" => VerticalMovement(x, y) || HorizontalMovement(x, y) || DiagonalMovement(x, y),
                "rook" => VerticalMovement(x, y) || HorizontalMovement(x, y),
                "king" => KingMovement(x, y),
                _ => false,
            };
        }

        return false;
    }

    public bool IsCaptureSquare(int x, int y)
    {
        return !controller.PositionIsEmpty(x, y) && (player == "white" && controller.GetPosition(x, y).GetComponent<Chesspiece>().GetPlayer() == "black" || player == "black" && controller.GetPosition(x, y).GetComponent<Chesspiece>().GetPlayer() == "white");
    }

    public List<BoardPosition> VerticalMovementSquares()
    {
        List<BoardPosition> availableSquares = new();

        // Up
        int i = y + 1;
        while (i < 8 && (controller.PositionIsEmpty(x, i) || IsCaptureSquare(x, i)))
        {
            availableSquares.Add(new BoardPosition(x, i));

            if (IsCaptureSquare(x, i)) break;

            i++;
        }

        // Down
        i = y - 1;
        while (i >= 0 && (controller.PositionIsEmpty(x, i) || IsCaptureSquare(x, i)))
        {
            availableSquares.Add(new BoardPosition(x, i));

            if (IsCaptureSquare(x, i)) break;

            i--;
        }

        return availableSquares;
    }

    public List<BoardPosition> HorizontalMovementSquares()
    {
        List<BoardPosition> availableSquares = new();
        int i;

        // Right
        i = x + 1;
        while (i < 8 && (controller.PositionIsEmpty(i, y) || IsCaptureSquare(i, y)))
        {
            availableSquares.Add(new BoardPosition(i, y));

            if (IsCaptureSquare(i, y)) break;

            i++;
        }

        // Left
        i = x - 1;
        while (i >= 0 && (controller.PositionIsEmpty(i, y) || IsCaptureSquare(i, y)))
        {
            availableSquares.Add(new BoardPosition(i, y));

            if (IsCaptureSquare(i, y)) break;

            i--;
        }

        return availableSquares;
    }

    public List<BoardPosition> DiagonalMovementSquares()
    {
        List<BoardPosition> availableSquares = new();
        int i, j;

        // Top left
        i = x - 1; j = y + 1;
        while (i >= 0 && j < 8 && (controller.PositionIsEmpty(i, j) || IsCaptureSquare(i, j)))
        {
            availableSquares.Add(new BoardPosition(i, j));

            if (IsCaptureSquare(i, j)) break;

            i--; j++;
        }

        // Top right
        i = x + 1; j = y + 1;
        while (i < 8 && j < 8 && (controller.PositionIsEmpty(i, j) || IsCaptureSquare(i, j)))
        {
            availableSquares.Add(new BoardPosition(i, j));

            if (IsCaptureSquare(i, j)) break;

            i++; j++;
        }

        // Bottom right
        i = x + 1; j = y - 1;
        while (i < 8 && j >= 0 && (controller.PositionIsEmpty(i, j) || IsCaptureSquare(i, j)))
        {
            availableSquares.Add(new BoardPosition(i, j));

            if (IsCaptureSquare(i, j)) break;

            i++; j--;
        }

        // Bottom left
        i = x - 1; j = y - 1;
        while (i >= 0 && j >= 0 && (controller.PositionIsEmpty(i, j) || IsCaptureSquare(i, j)))
        {
            availableSquares.Add(new BoardPosition(i, j));

            if (IsCaptureSquare(i, j)) break;

            i--; j--;
        }

        foreach (BoardPosition square in availableSquares)
        {
            Debug.Log(square.x + " " + square.y);
        }

        return availableSquares;
    }

    public List<BoardPosition> KnightMovementSquares()
    {
        List<BoardPosition> availableSquares = new();

        int[] dx = { 1, 2, 2, 1, -1, -2, -2, -1 };
        int[] dy = { -2, -1, 1, 2, 2, 1, -1, -2 };

        // Every possible knight move
        for (int i = 0; i < 8; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8 &&
                (controller.PositionIsEmpty(newX, newY) || IsCaptureSquare(newX, newY)))
            {
                availableSquares.Add(new BoardPosition(newX, newY));
            }
        }

        foreach (BoardPosition square in availableSquares)
        {
            Debug.Log(square.x + " " + square.y);
        }

        return availableSquares;
    }

    public List<BoardPosition> PawnMovementSquares()
    {
        List<BoardPosition> availableSquares = new();

        int direction = (player == "white") ? 1 : -1;

        if (y + direction >= 0 && y + direction < 8)
        {
            // Diagonal Movement
            if (x - 1 >= 0 && IsCaptureSquare(x - 1, y + direction)) availableSquares.Add(new BoardPosition(x - 1, y + direction));
            if (x + 1 < 8 && IsCaptureSquare(x + 1, y + direction)) availableSquares.Add(new BoardPosition(x + 1, y + direction));

            // Vertical Movement

            // One step
            if (controller.PositionIsEmpty(x, y + direction)) availableSquares.Add(new BoardPosition(x, y + direction));

            // Two steps
            if (y + 2 * direction >= 0 && y + 2 * direction < 8)
            {
                if (timesMoved == 0 &&
                    controller.PositionIsEmpty(x, y + direction) &&
                    controller.PositionIsEmpty(x, y + 2 * direction))
                {
                    availableSquares.Add(new BoardPosition(x, y + 2 * direction));
                }
            }
        }
        foreach (BoardPosition square in availableSquares)
        {
            Debug.Log(square.x + " " + square.y);
        }

        return availableSquares;
    }

    public List<BoardPosition> KingMovementSquares()
    {
        List<BoardPosition> availableSquares = new();

        int[] dx = { -1, 0, 1, 1, 1, 0, -1, -1 };
        int[] dy = { 1, 1, 1, 0, -1, -1, -1, 0 };

        for (int i = 0; i < 8; i++)
        {
            int newX = x + dx[i];
            int newY = y + dy[i];

            if (newX >= 0 && newX < 8 && newY >= 0 && newY < 8 &&
                (controller.PositionIsEmpty(newX, newY) || IsCaptureSquare(newX, newY)))
            {
                availableSquares.Add(new BoardPosition(newX, newY));
            }
        }

        int row = (player == "white") ? 0 : 7;
        if (timesMoved == 0)
        {
            if (controller.GetPosition(0, row).GetComponent<Chesspiece>().piece == "rook" &&
                controller.PositionIsEmpty(1, row) && controller.PositionIsEmpty(2, row) && controller.PositionIsEmpty(3, row))
            {
                availableSquares.Add(new BoardPosition(x - 2, y));
            }

            if (controller.GetPosition(7, row).GetComponent<Chesspiece>().piece == "rook" &&
                controller.PositionIsEmpty(5, row) && controller.PositionIsEmpty(6, row))
            {
                availableSquares.Add(new BoardPosition(x + 2, y));
            }
        }

        return availableSquares;
    }

    public bool VerticalMovement(int x, int y)
    {
        foreach (BoardPosition square in VerticalMovementSquares())
        {
            if (square.x == x && square.y == y) return true;
        }
        return false;
    }

    public bool HorizontalMovement(int x, int y)
    {
        foreach (BoardPosition square in HorizontalMovementSquares())
        {
            if (square.x == x && square.y == y) return true;
        }
        return false;
    }

    public bool DiagonalMovement(int x, int y)
    {
        foreach (BoardPosition square in DiagonalMovementSquares())
        {
            if (square.x == x && square.y == y) return true;
        }
        return false;
    }

    public bool KnightMovement(int x, int y)
    {
        foreach (BoardPosition square in KnightMovementSquares())
        {
            if (square.x == x && square.y == y) return true;
        }

        return false;
    }

    public bool PawnMovement(int x, int y)
    {
        foreach (BoardPosition square in PawnMovementSquares())
        {
            if (square.x == x && square.y == y) return true;
        }

        return false;
    }

    public bool KingMovement(int x, int y)
    {
        foreach (BoardPosition square in KingMovementSquares())
        {
            if (square.x == x && square.y == y)
            {
                if (this.x + 2 == square.x)
                {
                    Castle("king");
                }

                if (this.x - 2 == square.x)
                {
                    Castle("queen");
                }
                return true;
            }
        }

        return false;
    }

    public void Castle(string side)
    {
        int row = (player == "white") ? 0 : 7;
        if (side == "king")
        {
            Chesspiece rook = controller.GetPosition(7, row).GetComponent<Chesspiece>();

            rook.Move(5, row);
        }
        else if (side == "queen")
        {
            Chesspiece rook = controller.GetPosition(0, row).GetComponent<Chesspiece>();

            rook.Move(3, row);
        }
    }

    public bool Move(int x, int y)
    {
        // Reset position if move is invalid
        if (!CanMovePosition(x, y))
        {
            SetPosition(this.x, this.y);
            return false;
        };

        // Capture piece
        if (IsCaptureSquare(x, y))
        {
            if (controller.GetPosition(x, y).GetComponent<Chesspiece>().piece == "king")
            {
                controller.Winner(player);
            }
            Destroy(controller.GetPosition(x, y));
        }

        controller.SetPositionEmpty(this.x, this.y);

        timesMoved++;
        this.x = x;
        this.y = y;

        SetPosition(x, y);

        controller.SetPosition(gameObject);

        return true;
    }

    public string GetPlayer()
    {
        return player;
    }

    public void SetPosition(int x, int y)
    {
        transform.position = new Vector3(x * 0.16f - 0.56f, y * 0.16f - 0.56f, 0);
    }

    public BoardPosition GetPosition()
    {
        return new BoardPosition(x, y);
    }
}
