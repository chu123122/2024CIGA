using Tween;
using UnityEngine;
using UnityEngine.UIElements;

namespace Objects
{
    public class SnakeToWards
    {
        public readonly SnakeToward UpLeft = new SnakeToward(false,true, Quaternion.Euler(0, 0, 0));//
        public readonly SnakeToward UpRight=new SnakeToward(true,false,Quaternion.Euler(0, 0, 0));//
        public readonly SnakeToward DownLeft=new SnakeToward(false,true,Quaternion.Euler(0, 0, 0));//
        public readonly SnakeToward DownRight=new SnakeToward(true,false,Quaternion.Euler(0, 0, 0));//
        public readonly SnakeToward LeftDown=new SnakeToward(true,false,Quaternion.Euler(0, 0, 270));
        public readonly SnakeToward LeftUp=new SnakeToward(true,false,Quaternion.Euler(0, 0, 90));//
        public readonly SnakeToward RightUp=new SnakeToward(true,false,Quaternion.Euler(0, 0, 90));//
        public readonly SnakeToward RightDown=new SnakeToward(true,false,Quaternion.Euler(0, 0, 270));
    }
    public class SnakeToward
    { 
        public bool XFlip{ get; set; }
        public bool YFlip{ get; set; }
        public Quaternion Quaternion{ get; set; }
        
        public SnakeToward(bool xFlip,bool yFlip, Quaternion quaternion)
        {
            XFlip = xFlip;
            YFlip = yFlip;
            Quaternion = quaternion;
        }
    }
}