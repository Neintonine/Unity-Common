using UnityEngine;

namespace Common.Runtime
{
    public class StaticMonoBehaviour<T> : MonoBehaviour
        where T : MonoBehaviour
    {
        public static T Instance => _instance;

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
        private static GameObject _staticGameObject;
        
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
            if (!_staticGameObject)
            {
                _staticGameObject = new GameObject("_StaticComponents", typeof(T));
                return;
            }

            _staticGameObject.AddComponent<T>();
        }
    }
}