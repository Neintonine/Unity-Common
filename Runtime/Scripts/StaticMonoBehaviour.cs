using UnityEngine;

namespace Common.Runtime
{
    public class StaticMonoBehaviour<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public static T Instance => _instance;

        public bool AutoSpawn { get; protected set; } = false;
        protected static T _instance;
        public static bool IsUsable => _instance;
        
        protected virtual void OnEnable()
        {
            if (_instance == null) _instance = (T)(this as MonoBehaviour);
        }

        protected virtual void OnDisable()
        {
            if (!_instance) _instance = null;
        }
    }

    public class AutoStaticMonoBehaviour<T> : StaticMonoBehaviour<T>
        where T : MonoBehaviour
    {
        public new static T Instance
        {
            get
            {
                if (!_instance)
                {
                    Create();
                }
                return _instance;
            }
        }

        public static void Create()
        {
            new GameObject($"_{typeof(T).Name}", typeof(T));
        }
    }
}