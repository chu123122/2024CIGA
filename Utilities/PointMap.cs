using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utilities
{
    public class PointMap
    {
        public PointMap(int width, int height)
        {
            Map = new Point[width, height];
            Initialize(Map.GetLength(0), Map.GetLength(1));
        }

        private Point[,] Map { get; set; }

//------------------------------------------------公开方法------------------------------------------------------------------//

        public bool ContainsPoint(Point checkPoint)
        {
            return Map[checkPoint.XPosition, checkPoint.YPosition] != null;
        }
        
        public Point[] GetNearlyPoint(Point currentPoint)
        {
            int x = currentPoint.XPosition;
            int y = currentPoint.YPosition;
            Point[] nearPoints =
            {
                GetPointInMap(new Point(x - 1, y),true,true),//左
                GetPointInMap(new Point(x + 1, y),true,true),//右
                GetPointInMap(new Point(x, y + 1),true,true),//上
                GetPointInMap(new Point(x, y - 1),true,true)//下
            };
            Point[] validNearPoints = nearPoints.Where(p => p != null).ToArray();
            return validNearPoints;
        }

        public void GetNearlyPoint(Point currentPoint, out Point right,out Point left, out Point up,out Point down)
        {
            int x = currentPoint.XPosition;
            int y = currentPoint.YPosition;
            right = GetPointInMap(new Point(x + 1, y), true, true);//右
            left = GetPointInMap(new Point(x - 1, y), true, true);//左
            up=  GetPointInMap(new Point(x, y + 1), true, true);//上
            down = GetPointInMap(new Point(x, y - 1), true, true); //下
        }

        public int GetPointDirectionObstacle(Point currentPoint,Vector2 direction)
        {
            int x = (int)(currentPoint.XPosition + direction.x);
            int y = (int)(currentPoint.YPosition + direction.y);
            Point checkPoint = GetPointInMap(new Point(x,y), true, true);
            bool xOutOfRange = x > GetMapRange().x || x < 0;
            bool yOutOfRange = y > GetMapRange().y || y < 0;
            if (xOutOfRange || yOutOfRange) return 0;
            if(checkPoint!=null) return 0;
            else
            {
                return 1 + GetPointDirectionObstacle(new Point(x,y), direction);
            }
           
        }

        //获取地图的范围，返回一个Vector2的范围
        public Vector2 GetMapRange()
        {
            return new Vector2(Map.GetLength(0), Map.GetLength(1));
        }
        
        //获取地图上的点，返回Point
        public Point GetPointInMap(Point point,bool Inisitite=false,bool checkSelf=false)
        {
            int xLimit = Map.GetLength(0) - 1;
            int yLimit = Map.GetLength(1) - 1;
            bool xCan = point.XPosition >= 0 && point.XPosition <= xLimit;
            bool yCan = point.YPosition >= 0 && point.YPosition <= yLimit;
            bool inSelf=false;
            foreach (var snakeBase in Snake.SnakeBases)
            {
                if (point.Equals(snakeBase.Points[1]))
                    inSelf = true;
            }
            if (xCan && yCan&&(!inSelf||!checkSelf)) return Map[point.XPosition, point.YPosition];
            if(Inisitite==false)
                throw new ArgumentException($"点({point.XPosition},{point.YPosition})的位置不在地图上！");
            return null;
        }

        //检测两个点是否在同一位置，返回True/False
        public bool CheckPositionIsSame(Point currentPoint, Point targetPoint)
        {
            //确定点是否有问题
            if (targetPoint == null || targetPoint.CanMove == false)
                throw new Exception("目标点无法移动或目标点不存在");

            bool xPosition = currentPoint.XPosition == targetPoint.XPosition;
            bool yPosition = currentPoint.YPosition == targetPoint.YPosition;
            if (xPosition && yPosition)
                return true;
            else
                return false;
        }

        public void InitializePointCannotMove(List<Point> points)
        {
            foreach (var point in points)
            {
                int x = point.XPosition;
                int y = point.YPosition;
                Map[x, y].CanMove = false;
            }
        }

//------------------------------------------------私有方法------------------------------------------------------------------//
        //初始化地图中点
        private void Initialize(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Map[i, j] = new Point(i, j, true);
                }
            }

            InitializePointCannotMove();
        }

        //初始化不可以移动的点
        private void InitializePointCannotMove()
        {
            GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (var obstacle in obstacles)
            {
                var position = obstacle.transform.position;
                int x = (int)position.x;
                int y = (int)position.y;
                Map[x, y].CanMove = false;
            }
        }
    }
}