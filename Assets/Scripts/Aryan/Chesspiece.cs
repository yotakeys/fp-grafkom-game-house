using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
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
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            switch (piece)
            {
                case "pawn":
                    return PawnMovement(x, y);

            }
        }

        return false;
    }

    public bool PawnMovement(int x, int y)
    {
        // Diagonal Movement
        if (Mathf.Abs(x - this.x) == 1 && y == this.y + 1)
        {
            // Check if there is piece on diagonal
            if (!controller.PositionIsEmpty(x, y) && controller.GetPosition(x, y).GetComponent<Chesspiece>().GetPlayer() == "black")
            {
                return true;
            }
        }

        // Vertical Movement
        if (x == this.x && y > this.y)
        {
            if (controller.PositionIsEmpty(x, y))
            {
                if (y <= this.y + 2 && timesMoved == 0) return true;
                if (y <= this.y + 1) return true;
            }
        }
        return false;
    }

    public void Move(int x, int y)
    {
        if (!CanMovePosition(x, y))
        {
            SetPosition(this.x, this.y);
            return;
        };

        timesMoved++;
        this.x = x;
        this.y = y;

        SetPosition(x, y);
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
        return new BoardPosition(this.x, this.y);
    }
}
