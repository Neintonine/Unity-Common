using UnityEditor;
using UnityEngine;

namespace Common.Runtime
{
    public class SingletonScriptableObject<T> : UnityEngine.ScriptableObject where T : UnityEngine.ScriptableObject, new()
    {
        private static T _instance; 

        public static T Instance
        {
            get
            {
                if (_instance == null) _instance = Resources.Load<T>(typeof(T).Name);
                return _instance;
            }
        }
        public static bool IsAvailable
        {
            get
            {
                if (_instance != null) return true;
                if (Resources.FindObjectsOfTypeAll<T>().Length > 0) return true;
                return false;
            }
        }

#if UNITY_EDITOR
        
        public static void CreateAsset()
        {
            _instance = ScriptableObject.CreateInstance<T>();
            if (!AssetDatabase.IsValidFolder("Assets/Resources")) AssetDatabase.CreateFolder("Assets","Resources");
            AssetDatabase.CreateAsset(_instance, "Assets/Resources/"+typeof(T).Name+".asset");
        }
        
#endif
        
    }
}
