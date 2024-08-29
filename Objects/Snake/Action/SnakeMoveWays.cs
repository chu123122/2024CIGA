using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Objects
{
    public class SnakeMoveWays
    {
        /// <summary>
        /// 返回从当前位置到目标点的最近移动路径,依据BFS行动
        /// </summary>
        /// <param name="start">当前位置</param>
        /// <param name="target">目标位置</param>
        public Point MoveToTarget(Point start, Point target)
        {
            Dictionary<Point, Point> cameFrom = new Dictionary<Point, Point>();
            Queue<Point> queue = new Queue<Point>();
            queue.Enqueue(start);
            cameFrom[start] = new Point(-1, -1);
            while (queue.Count > 0)
            {
                PointMap map = GlobalGame.Instance.PointMap;
                Point current = queue.Dequeue();
                if (map.CheckPositionIsSame(current, target))
                {
                    return ReturnListFromDic(cameFrom, current)[1];
                }

                Point[] nearPoints = map.GetNearlyPoint(current);
                foreach (var neighbor in nearPoints)
                {
                    if (!cameFrom.ContainsKey(neighbor) && neighbor.CanMove)
                    {
                        cameFrom[neighbor] = current;
                        queue.Enqueue(neighbor);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 返回下一次移动到的位置，依据默认直行行动
        /// </summary>
        /// <param name="current"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        public Point MoveWithoutTarget(Point current, PointMap map)
        {
            map.GetNearlyPoint(current, out Point right, out Point left, out Point up, out Point down);
            Point[] points = map.GetNearlyPoint(current);//
            if(points.Length==1)return points[0];
            SnakeDirection currentDir= GlobalGame.Instance.Head.Direction[1];//
            Vector2 direction = currentDir switch
            {
                SnakeDirection.Up => Vector2.up,
                SnakeDirection.Down => Vector2.down,
                SnakeDirection.Left => Vector2.left,
                SnakeDirection.Right => Vector2.right,
                _ => throw new ArgumentOutOfRangeException()
            };
            foreach(var point in points)
            {
                bool xIsSame = point.XPosition-current.XPosition == (int)direction.x;
                bool yIsSame = point.YPosition-current.YPosition == (int)direction.y;
                if (xIsSame && yIsSame)
                    return point;
            }
            if (right == null)
            {
                if (up == null && down == null &&left != null)
                    return left;
                Point check=new Point(current.XPosition+1, current.YPosition);
                int upObstacles=map.GetPointDirectionObstacle(check,Vector2.up);
                int downObstacles=map.GetPointDirectionObstacle(check,Vector2.down);
                return upObstacles>=downObstacles ?up:down;
            }
            else
            {
                return right;
            }

            if (up == null || down == null)
            {
                int rightObstacles=1;
                int leftObstacles=1;
                return rightObstacles>=leftObstacles ?right:left;
            }
            
            Point rightNext=new Point(current.XPosition+1, current.YPosition);
            Point leftNext=new Point(current.XPosition-1, current.YPosition);
            Point upNext=new Point(current.XPosition, current.YPosition+1);
            Point downNext=new Point(current.XPosition, current.YPosition-1);
            if (map.GetPointInMap(rightNext,true,true) != null)
                return rightNext;
            if(map.GetPointInMap(upNext,true,true) != null)
                return upNext;
            if(map.GetPointInMap(leftNext,true,true) != null)
                return leftNext;
            if(map.GetPointInMap(downNext,true,true)!= null)
                return downNext;
            throw new ArgumentException("没有找到下一个可以行动的点");
        }


        private List<Point> ReturnListFromDic(Dictionary<Point, Point> cameFrom, Point current)
        {
            List<Point> paths = new List<Point>();
            while (current != null)
            {
                paths.Add(current);
                Point last = new Point(0, 0);
                current = TryGetPointFromDic(cameFrom, current);
                if (current.XPosition == -1)
                    break;
            }

            paths.Reverse();
            return paths;
        }

        private Point TryGetPointFromDic(Dictionary<Point, Point> dic, Point current)
        {
            Point last = new Point(0, 0);
            dic.TryGetValue(current, out last);
            if (last != null)
            {
                return dic[current];
            }
            else
            {
                throw new ArgumentException("无法找到当前点的前一点");
            }
        }
    }
}