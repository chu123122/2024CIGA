using System;
using Objects;
using Tween;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utilities
{
    public class GlobalGame : MonoSingleton<GlobalGame>
    {
        public readonly SnakeToWards SnakeToWards = new SnakeToWards();
        public PointMap PointMap { get; set; }
        public SnakeHead Head { get; set; }

        protected override void Awake()
        {
            base.Awake();
            AwakeMap();
            Head = GameObject.FindGameObjectWithTag("SnakeHead").GetComponent<SnakeHead>();
            Head.Points[1] = new Point(Head.gameObject.transform);
            Snake.InsititeSnake();
        }

        private void AwakeMap()
        {
            int sceneNumber = SceneManager.GetActiveScene().buildIndex;
            switch (sceneNumber)
            {
                case 1:PointMap = new PointMap(4, 3);
                    break;
                case 2: PointMap = new PointMap(6, 4);
                    break;
                case 3:PointMap = new PointMap(18, 9);
                    break;
                case 4:PointMap = new PointMap(18, 9);
                    break;
                case 0:PointMap = new PointMap(18, 9);
                    break;
                default: throw new ArgumentOutOfRangeException($"地图初始化错误");
            }
        }

        public void TestAwake()
        {
            Awake();
        }
        
    }
}