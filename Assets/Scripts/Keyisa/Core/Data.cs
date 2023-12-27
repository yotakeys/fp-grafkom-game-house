using UnityEngine;

public static class Data
{
    public static int scores = 0;
    public static int lifes = 3;

    public static float minX = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
    public static float maxX = Camera.main.ViewportToWorldPoint(Vector3.right).x;
    public static float minY = Camera.main.ViewportToWorldPoint(Vector3.zero).y;
    public static float maxY = Camera.main.ViewportToWorldPoint(Vector3.up).y;
}