using System;
using Objects;
using UnityEngine;
using Utilities;

public class SnakeHead : SnakeBase
{
    public event Action SnakeHeadMoved;
    private void Start()
    {
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        InitializePoints(new Point(x-1,y),new Point(x,y));
    }

    private void Update()
    {
        InputMove();
    }

    private void InputMove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Point target =
                GlobalGame.Instance.PointMap.GetPointInMap(new Point(Points[1].XPosition, Points[1].YPosition + 1));
            SnakeMove_Head(target);
            SnakeHeadMoved?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Point target =
                GlobalGame.Instance.PointMap.GetPointInMap(new Point(Points[1].XPosition - 1, Points[1].YPosition));
            SnakeMove_Head(target);
            SnakeHeadMoved?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Point target =
                GlobalGame.Instance.PointMap.GetPointInMap(new Point(Points[1].XPosition, Points[1].YPosition - 1));
            SnakeMove_Head(target);
            SnakeHeadMoved?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Point target =
                GlobalGame.Instance.PointMap.GetPointInMap(new Point(Points[1].XPosition + 1, Points[1].YPosition));
            SnakeMove_Head(target);
            SnakeHeadMoved?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Snake.GetAllNextPoint();
        }
    }
    
}