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
        _renderer = this.GetComponent<LineRenderer>();
    }

    private LineRenderer _renderer;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _renderer.SetPosition(0,new Vector3(5,0,0));
            _renderer.SetPosition(1,new Vector3(0,0,0));
        }
    }
    
}