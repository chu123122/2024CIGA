using System;
using System.Collections.Generic;
using System.Linq;
using Objects;
using Tween;
using UnityEngine;
using Utilities;
using Object = UnityEngine.Object;

public static class Snake
{
    public static  List<SnakeBase> SnakeBases;
    public static  List<Transform> SnakeTransform;

    public static void InsititeSnake()
    {
        SnakeBase[] snakeBases = Object.FindObjectsByType<SnakeBase>(FindObjectsSortMode.None);
        List<Transform> snakeTransform = new List<Transform>();
        snakeTransform.AddRange(snakeBases.Select(snakeBase => snakeBase.transform));
        DistanceComparer comparer = new DistanceComparer();
        snakeTransform.Sort(comparer);
        SnakeTransform = snakeTransform;
        SnakeBases=new List<SnakeBase>();
        foreach(var snake in SnakeTransform)
        {
            SnakeBases.Add(snake.gameObject.GetComponent<SnakeBase>());
        }
    }
    
    public static void MoveAllSnakeBases(Queue<Point> next)
    {
        foreach (var @base in SnakeBases)
        {
            Point target=new Point(0,0);
            if (next.Count > 0)
            {
                bool get = false;
                TweenManager.Instance.StartProcedure(SKCurve.LinearIn, 0.8f, (t) =>
                {
                    if (next.Count > 0&&!get)
                    {
                        get = true;
                        Point nextPoint=next.Dequeue();
                        target = nextPoint;
                        @base.SnakeMove(target);
                    }
                    //弹出来的可能不是地图上的点，可能有bug
                    // 设置目标位置
                    Transform transform = @base.gameObject.transform;
                    Vector2 nextPosition = new Vector2(target.XPosition, target.YPosition);
                    transform.position = Vector2.Lerp(transform.position, nextPosition, t);
                }, () =>
                {
                    //Debug.Log("Finished");
                });
            }
        }
    }
    //计算后续部分位置的算法存在问题
    public static Queue<Point> GetAllNextPoint(Vector2 direction=default)
    {
        if (direction == default)direction=Vector2.right;
        Vector2 head = GameObject.FindWithTag("SnakeHead").transform.position;
        Point changePoint = GlobalGame.Instance.PointMap.GetPointInMap(new Point((int)head.x,(int)head.y),true);
        changePoint.SetDirection(direction);
        
        Queue<Point> nextQueue = new Queue<Point>();
        foreach (var t in SnakeTransform)
        {
            var position = t.position;
            Point currentPoint = GlobalGame.Instance.PointMap.GetPointInMap(new Point((int)position.x,(int)position.y),true);
            if (currentPoint != null)
            {
                Point nextPoint0 = new Point(currentPoint.XPosition + (int)currentPoint.Direction.x,
                    currentPoint.YPosition + (int)currentPoint.Direction.y);
                Point nextPoint = GlobalGame.Instance.PointMap.GetPointInMap(nextPoint0);
                nextQueue.Enqueue(nextPoint);
            }
            else
            {
                Vector2 dir =  Vector2.right;
                Point nextPoint = new Point((int)(position.x+dir.x), (int)(position.y+dir.y));
                nextQueue.Enqueue(nextPoint);
            }
        }
        return nextQueue;
        //获取所有Snake的物体
        //获取它们的Transform
        //获取它们所在当前点
        //从它们所在当前点获取方向，计算出它们要移动的下一个点
    }

    private class DistanceComparer : IComparer<Transform>
    {

        public int Compare(Transform x, Transform y)
        {
            if (x == null || y == null) throw new ArgumentNullException($"比较的对象Transform为空");
            //注意要先实例化Global里的Head的上一个点
            Point headCurrent = GlobalGame.Instance.Head.Points[1];
            float distanceX = Vector2.Distance(x.position,new Vector2(headCurrent.XPosition, headCurrent.YPosition));
            float distanceY = Vector2.Distance(y.position,new Vector2(headCurrent.XPosition, headCurrent.YPosition));

            return distanceX.CompareTo(distanceY);
        }
    }
}