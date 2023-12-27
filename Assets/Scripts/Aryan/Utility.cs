using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

public struct BoardPosition
{
    public int x, y;

    public BoardPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
}
public class Utility
{
    public static BoardPosition CoordinatesToBoardPosition(Vector3 position)
    {
        float relativeX = position.x - (-0.64f);
        float relativeY = position.y - (-0.64f);

        BoardPosition boardPosition = new(Mathf.FloorToInt(relativeX / 0.16f), Mathf.FloorToInt(relativeY / 0.16f));

        return boardPosition;
    }
}
