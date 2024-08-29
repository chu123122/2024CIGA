using System;
using Objects;
using UnityEngine;
using Utilities;

public class SnakeHead : SnakeBase
{
    private void Start()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        InitializePoints(new Point(x-1,y),new Point(x,y));
    }
}