using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Objects;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace Utilities
{
    public class Point
    {
        private bool _canMove;

        private Vector2 _direction;

        public Point(int xPosition, int yPosition, bool canMove = true, Vector2 direction = default)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            CanMove = canMove;
            _direction = direction == Vector2.zero ? Vector2.right : direction;
        }

        public Point(Transform transform, bool canMove = true, Vector2 direction = default)
        {
            var position = transform.position;
            XPosition = (int)position.x;
            YPosition = (int)position.y;
            CanMove = canMove;
            _direction = direction == Vector2.zero ? Vector2.right : direction;
        }

        public int XPosition { get; }
        public int YPosition { get; }

        public Vector2 Direction
        {
            get => _direction;
            set
            {
                if (value != Vector2.up && value != Vector2.down && value != Vector2.left && value != Vector2.right)
                {
                    throw new ArgumentException("Invalid direction");
                }

                _direction = value;
            }
        }

        public bool CanMove
        {
            get
            {
                Vector2 mapRange = GlobalGame.Instance.PointMap.GetMapRange();
                bool xCrossBorder = XPosition > mapRange.x || XPosition < 0;
                bool yCrossBorder = YPosition > mapRange.y || YPosition < 0;
                if (xCrossBorder || yCrossBorder)
                {
                    Debug.Log($"<color=red>超出地图边界：({XPosition},{YPosition})</color>");
                    _canMove = false;
                }

                return _canMove;
            }

            set => _canMove = value;
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        public override string ToString()
        {
            return $"(X:{XPosition},Y:{YPosition})";
        }

        public static string ToStrings(Point[] points)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var point in points)
            {
                sb.Append(point.ToString());
            }

            return sb.ToString();
        }

        public static string ToSeceletString(Point[] points, Func<Point, bool> function)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var point in points)
            {
                if (function(point))
                    sb.Append(point.ToString());
            }

            return sb.ToString();
        }
    }
}