using UnityEditor;
using UnityEngine;

namespace Common.Runtime
{
    [CreateAssetMenu(fileName = "MarkerHandler", menuName = "Configurations/MarkerHandler", order = 0)]
    public class MarkerHandler : SingletonScriptableObject<MarkerHandler>
    {
        public int MarkerLayer;
        public GameObject HeadingMarker;
        public Mesh XMarker;
        public GameObject boundaryObject;
#if UNITY_EDITOR
        [MenuItem("GameObject/Marker/Heading Marker",false, 10)]
        static void CreateHeadingMarker()
        {
            GenerateObject(Instance.HeadingMarker);
        }
        
        [MenuItem("GameObject/Marker/Enemy Boundary",false, 10)]
        static void CreateEnemyBoundary()
        {
            GenerateObject(Instance.boundaryObject);

        }
        
        static void GenerateObject(GameObject prefab) 
        {
            GameObject obj = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            
            if (!Selection.activeTransform) return;
            obj.transform.position = Selection.activeTransform.position;
            obj.transform.rotation = Selection.activeTransform.rotation;
        }
        
#endif 
    }
}