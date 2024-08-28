using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Objects
{
    public class SnakeBase : MonoBehaviour 
    {
        [SerializeField] private List<Sprite> sprites = new List<Sprite>();
        private readonly Point[] _points = new Point[2];
        private SnakeDirection[] _directions = new SnakeDirection[2];
        private SnakeState _snakeState;
        protected SnakeToward CurrentToward;
        
        public Point[] Points => _points;

        public SnakeDirection[] Direction
        {
            get => _directions;
            set => _directions = value;
        }

        //确定当前是否在转弯
        public SnakeState SnakeState
        {
            get
            {
                _snakeState = DetermineState(Direction[0], Direction[1]);
                return _snakeState;
            }
            set => _snakeState = value;
        }

        public void InitializeDir(SnakeDirection previousDir, SnakeDirection currentDir)
        {
            _directions[0] = previousDir;
            _directions[1] = currentDir;
        }

        public void InitializePoints(Point previousPoints, Point currentPoints)
        {
            _points[0] = previousPoints;
            _points[1] = currentPoints;

            _directions[0] = DetermineDirection(_points[0], _points[1]);
            _directions[1] = DetermineDirection(_points[0], _points[1]);
        }

        public string ToStringDirAndState()
        {
            return $"Previous Dir: {Direction[0]}, Current Dir: {Direction[1]}, Snake State: {SnakeState}";
        }

        public string ToStringDirAndState1()
        {
            return $"Previous Dir: {_directions[0]}, Current Dir: {_directions[1]}, Snake State: {_snakeState}";
        }

        public string ToStringPoints()
        {
            return $"Previous Points: {_points[0]}, Current Points: {_points[1]}";
        }

        public Point GetCurrentPoint()
        {
            return _points[1];
        }



        /// <summary>
        /// 蛇的逻辑位置改变（物理位置待改变）
        /// </summary>
        /// <param name="targetPosition"></param>
        public void SnakeMove(Point targetPosition)
        {
            SetNewPoints(targetPosition);
            RefreshDirection();
            if(!this.GetComponent<SnakeHead>())
                this.GetComponent<SpriteRenderer>().sprite=DetermineSprite(SnakeState);
            SwitchSnakeFollow();
        }

// --------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 改变蛇的朝向
        /// </summary>
        private void SwitchSnakeFollow()
        {
            if (Direction[0] == Direction[1]) return;
            CurrentToward = SwitchSnakeToward();
            this.GetComponent<SpriteRenderer>().flipX = CurrentToward.XFlip;
            this.GetComponent<SpriteRenderer>().flipY = CurrentToward.YFlip;
            this.transform.rotation = CurrentToward.Quaternion;
        }

        private SnakeToward SwitchSnakeToward()
        {
            return (Dir1: Direction[0], Dir2: Direction[1]) switch
            {
                (SnakeDirection.Up, SnakeDirection.Left) => GlobalGame.Instance.SnakeToWards.UpLeft,
                (SnakeDirection.Up, SnakeDirection.Right) => GlobalGame.Instance.SnakeToWards.UpRight,
                (SnakeDirection.Down, SnakeDirection.Left) => GlobalGame.Instance.SnakeToWards.DownLeft,
                (SnakeDirection.Down, SnakeDirection.Right) => GlobalGame.Instance.SnakeToWards.DownRight,
                (SnakeDirection.Left, SnakeDirection.Down) => GlobalGame.Instance.SnakeToWards.LeftDown,
                (SnakeDirection.Left, SnakeDirection.Up) => GlobalGame.Instance.SnakeToWards.LeftUp,
                (SnakeDirection.Right, SnakeDirection.Up) => GlobalGame.Instance.SnakeToWards.RightUp,
                (SnakeDirection.Right, SnakeDirection.Down) => GlobalGame.Instance.SnakeToWards.RightDown,
                _ => throw new ArgumentOutOfRangeException($"转向判断错误")
            };
        }

        /// <summary>
        /// 更新方向
        /// </summary>
        private void RefreshDirection()
        {
            _directions[0] = _directions[1];
            _directions[1] = DetermineDirection(_points[0], _points[1]);
        }

        /// <summary>
        /// 根据目标点，更新移动到目标点后的PreviousPoint和CurrentPoint
        /// </summary>
        /// <param name="targetPosition"></param>
        private void SetNewPoints(Point targetPosition)
        {
            _points[0] = _points[1];
            _points[1] = targetPosition;
        }

        /// <summary>
        /// 依据previousSnakeDir和currentSnakeDir返回当前拐弯的方向或不在拐弯
        /// </summary>
        /// <param name="previousSnakeDir"></param>
        /// <param name="currentSnakeDir"></param>
        /// <returns></returns>
        private SnakeState DetermineState(SnakeDirection previousSnakeDir, SnakeDirection currentSnakeDir)
        {
            return (Dir1: previousSnakeDir, Dir2: currentSnakeDir) switch
            {
                (SnakeDirection.Up, SnakeDirection.Left) => SnakeState.UpLeft,
                (SnakeDirection.Up, SnakeDirection.Right) => SnakeState.UpRight,
                (SnakeDirection.Down, SnakeDirection.Left) => SnakeState.DownLeft,
                (SnakeDirection.Down, SnakeDirection.Right) => SnakeState.DownRight,
                (SnakeDirection.Left, SnakeDirection.Down) => SnakeState.LeftDown,
                (SnakeDirection.Left, SnakeDirection.Up) => SnakeState.LeftUp,
                (SnakeDirection.Right, SnakeDirection.Up) => SnakeState.RightUp,
                (SnakeDirection.Right, SnakeDirection.Down) => SnakeState.RightDown,
                _ => SnakeState.Normal
            };
        }

        private Sprite DetermineSprite(SnakeState state)
        {
            return state switch
            {
                SnakeState.UpLeft or SnakeState.DownRight or SnakeState.LeftDown or SnakeState.RightUp => sprites.Find(
                    sprite => sprite.name.Contains("2")),
                SnakeState.UpRight or SnakeState.DownLeft or SnakeState.LeftUp or SnakeState.RightDown => sprites.Find(
                    sprite => sprite.name.Contains("1")),
                SnakeState.Normal => sprites.Find(sprite => sprite.name.Contains("常规")),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }

        /// <summary>
        /// 依据previousPoint和currentPoint返回当前方向
        /// </summary>
        /// <param name="previousPoint"></param>
        /// <param name="currentPoint"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private SnakeDirection DetermineDirection(Point previousPoint, Point currentPoint)
        {
            int X = currentPoint.XPosition - previousPoint.XPosition;
            int Y = currentPoint.YPosition - previousPoint.YPosition;
            return (x: X, y: Y)switch
            {
                (0, 1) => SnakeDirection.Up,
                (0, -1) => SnakeDirection.Down,
                (1, 0) => SnakeDirection.Right,
                (-1, 0) => SnakeDirection.Left,
                _ => throw new ArgumentOutOfRangeException($"方向判定失败，" +
                                                           $"当前点:{new Vector2(previousPoint.XPosition, previousPoint.YPosition)}," +
                                                           $"目标点:{new Vector2(currentPoint.XPosition, currentPoint.YPosition)}")
            };
        }
        
    }
}