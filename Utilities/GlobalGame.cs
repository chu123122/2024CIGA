using System;
using Objects;
using Tween;
using UnityEngine;

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
            PointMap = new PointMap(18, 9);
            Head = GameObject.FindGameObjectWithTag("SnakeHead").GetComponent<SnakeHead>();
            Head.Points[1] = new Point(Head.gameObject.transform);
        }

        public void TestAwake()
        {
            Awake();
        }
        
    }
}