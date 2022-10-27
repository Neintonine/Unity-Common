using UnityEngine;

namespace Common.Runtime.Modifiers
{
    [AddComponentMenu("Modifiers/Array Modifier")]
    public class ArrayModifier : MonoBehaviour
    {
        public int Amount
        {
            get => _amount;
            set => _amount = value;
        }
        
        public GameObject SpawnedObject => _gameObject ? _gameObject : gameObject;

        [SerializeField] private bool _autoSpawn;

        [SerializeField] private int _amount = 1;
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _clearChildren = true;

        [SerializeField] private Vector3 _constantPosition;
        [SerializeField] private Vector3 _constantScale;
        
        [SerializeField] private Vector3 _addedPosition;
        [SerializeField] private Vector3 _addedScale;
        [SerializeField] private Transform _arrayObject;

        [SerializeField] private Mesh _mesh;
        [SerializeField] private int _previewAmount = -1;

        private void Awake()
        {
            if (!_autoSpawn) return;
            Spawn();
        }

        public GameObject[] Spawn()
        {
            GameObject spawnedObject = _gameObject ? _gameObject : gameObject;
            GameObject[] data = new GameObject[Amount + 1];
            data[0] = spawnedObject;
            
            for (int i = 1; i <= _amount; i++)
            {
                GameObject obj = Instantiate(spawnedObject, Vector3.zero, spawnedObject.transform.rotation, spawnedObject.transform);
                obj.layer = spawnedObject.layer;
                Transform objTransform = obj.transform;
                objTransform.localPosition = GetLocalPosition(i);
                objTransform.localScale = GetLocalScale(i);
                
                if (_clearChildren) objTransform.DestroyChildren();

                data[i] = obj;
            }

            return data;
        }

        private void OnDrawGizmos()
        {
            Transform tf = _gameObject ? _gameObject.transform : transform;
            
            int amount = _previewAmount >= 0 ? _previewAmount : _amount;

            UnityEngine.Gizmos.matrix = tf.localToWorldMatrix;
            bool hasMesh = _mesh;
            
            for (int i = 1; i <= _amount; i++)
            {
                if (hasMesh) UnityEngine.Gizmos.DrawMesh(_mesh, GetLocalPosition(i), Quaternion.identity, GetLocalScale(i));
                else UnityEngine.Gizmos.DrawCube(GetLocalPosition(i), Vector3.one);
            }
        }

        private Vector3 GetGlobalPosition(int index)
        {
            return transform.TransformPoint(GetLocalPosition(index));
        }

        private Vector3 GetGlobalScale(int index)
        {
            return transform.lossyScale + GetLocalScale(index);
        }
        
        private Vector3 GetLocalPosition(int index)
        {
            return _constantPosition + ((_addedPosition + (_arrayObject ? _arrayObject.localPosition : Vector3.zero)) * index);
        }
        
        private Vector3 GetLocalScale(int index)
        {
            return _constantScale + (Vector3.one + (_addedScale + (_arrayObject ? _arrayObject.localScale - Vector3.one : Vector3.zero)) * index);
        }
    }
}