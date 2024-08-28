using UnityEngine;
using Utilities;

namespace Objects
{
    public class SnakeTail : SnakeBase
    {
        [SerializeField] private SnakeBase last;
        private void Start()
        {
            int x = (int)transform.position.x;
            int y = (int)transform.position.y;
            InitializePoints(new Point(x-1,y),new Point(x,y));
        }
    }
}