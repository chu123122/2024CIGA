using System;
using System.Collections;
using Objects;
using UnityEngine;
using Utilities;

public class SnakeNeck : SnakeBase
{
    private SnakeHead _head;
    private void Start()
    {
        WaitForStartCoroutine(() => _head = GlobalGame.Instance.Head);
        int x = (int)transform.position.x;
        int y = (int)transform.position.y;
        InitializePoints(new Point(x-1,y),new Point(x,y));
    }

    private void WaitForStartCoroutine(Action action)
    {
        StartCoroutine(WaitForStart(action));
    }
    private IEnumerator WaitForStart(Action action)
    {
        bool passed=true;
        try
        {
            action?.Invoke();
        }
        catch(NullReferenceException exception)
        {
            passed = false;
            Debug.Log(exception);
        }
        yield return new WaitForSeconds(0.1f);
        if(passed) action?.Invoke();
    }
    
}