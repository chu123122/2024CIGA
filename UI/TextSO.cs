using System;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "TextSO", menuName = "ScriptableObject/TextSO")]
    public class TextSo:ScriptableObject
    {
        public One one;
        public Two two;
        public Three three;
         public Four four;
         public Fail fail;
         public Over over;
    }
    [Serializable]
    public class One
    {
        public string text1;
    }
    [Serializable]
    public class Two
    {
        public string text1;
    }
    [Serializable]
    public class Three
    {
        public string text1;
        public string text2;
        public string text3;
        public string text4;
        public string text5;
    }
    [Serializable]
    public class Four
    {
        public string text1;
        public string text2;
    }
    [Serializable]
    public class Fail
    {
        public string text1;
        public string text2;
    }
    [Serializable]
    public class Over
    {
        public string text1;
    }
}