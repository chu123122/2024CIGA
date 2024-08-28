using Objects;
using UnityEngine;
using Utilities;

public class SnakeNeck : SnakeBase
{
    private SnakeHead _head;
    private void Start()
    {
        _head = GlobalGame.Instance.Head;
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        InitializePoints(new Point(x-1,y),new Point(x,y));
    }
    
}