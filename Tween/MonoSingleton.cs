using UnityEngine;

namespace Tween
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T _instance;

        static MonoSingleton()
        {
        }

        protected MonoSingleton()
        {
        }

        public static T Instance
        {
            get => _instance;
        }

        protected virtual void Awake()
        {
            if (_instance == null)
                _instance = (T)this;
            else
                Destroy(gameObject);
        }
    }

    public class Singleton<T> where T : class, new()
    {
        private static T _instance = null;

        static Singleton()
        {
            
        }

        protected Singleton()
        {
        }

        public static T Instance => _instance ??= new T();

        protected void Clear()
        {
            _instance = null;
        }
    }
}