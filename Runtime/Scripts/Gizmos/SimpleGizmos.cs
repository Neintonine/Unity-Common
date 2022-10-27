using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Common.Runtime.Gizmos
{
    public enum SimpleGizmosType
    {
        Cube,
        WireCube,
        Frustum,
        GUITexture,
        Icon,
        Line,
        Mesh,
        WireMesh,
        Ray,
        Sphere,
        WireSphere,
    }

    [AddComponentMenu("Gizmos/Simple Gizmos")]
    public class SimpleGizmos : MonoBehaviour
    {
        public bool useTransform;

        public SimpleGizmosType renderType = SimpleGizmosType.Cube;
        public Color color = Color.white;

        public bool onSelected = false;

        // common
        private Vector3 corPosition => useTransform ? transform.position : position;
        public Vector3 position = Vector3.zero;
        private Quaternion rotQuaternion => useTransform ? transform.rotation : Quaternion.Euler(rotation);
        public Vector3 rotation = Vector3.zero;

        // cube / wirecube
        /// COMP: Position
        public Vector3 size = Vector3.one;

        // frustum
        /// COMP: Position
        /// COMP: Rotation
        public float fov = 60;
        public float minRange = 0;
        public float maxRange = 20;
        public float aspect = 1.666f;

        // gui texture
        public Rect screen = new Rect(0,0, 20,20);
        public Texture texture;
        public Material textureMaterial;

        // icon
        /// COMP: Position
        public string iconname = "";
        public bool allowScaling = false;

        // line
        public Vector3 to = Vector3.one;

        // mesh / wiremesh
        public Mesh mesh;
        public int submeshIndex = -1;
        /// COMP: Position
        /// COMP: Rotation
        public Vector3 scale = Vector3.one;

        // ray
        /// COMP: Position
        public Vector3 direction = Vector3.forward;

        // sphere / wiresphere
        /// COMP: Position
        public float radius = 1;

        private void OnDrawGizmos()
        {
            if (onSelected) return;

            RenderGizmos();
        }

        private void OnDrawGizmosSelected()
        {
            if (!onSelected) return;
            RenderGizmos();
        }

        private void RenderGizmos()
        {
            UnityEngine.Gizmos.color = color;

            switch(renderType)
            {
                case SimpleGizmosType.Cube:
                    UnityEngine.Gizmos.DrawCube(corPosition, size);
                    break;
                case SimpleGizmosType.WireCube:
                    UnityEngine.Gizmos.DrawWireCube(corPosition, size);
                    break;
                case SimpleGizmosType.Frustum:
                    UnityEngine.Gizmos.matrix = Matrix4x4.TRS(corPosition, rotQuaternion, Vector3.one);
                    UnityEngine.Gizmos.DrawFrustum(Vector3.zero, fov, maxRange, minRange, aspect);
                    break;
                case SimpleGizmosType.GUITexture:
                    UnityEngine.Gizmos.DrawGUITexture(screen, texture, textureMaterial);
                    break;
                case SimpleGizmosType.Icon:
                    UnityEngine.Gizmos.DrawIcon(corPosition, name, allowScaling);
                    break;
                case SimpleGizmosType.Line:
                    UnityEngine.Gizmos.DrawLine(corPosition, to);
                    break;
                case SimpleGizmosType.Mesh:
                    if (submeshIndex >= 0) UnityEngine.Gizmos.DrawMesh(mesh, submeshIndex, position, rotQuaternion, scale);
                    else UnityEngine.Gizmos.DrawMesh(mesh, corPosition, rotQuaternion, scale);
                    break;
                case SimpleGizmosType.WireMesh:
                    if (submeshIndex >= 0) UnityEngine.Gizmos.DrawWireMesh(mesh, submeshIndex, position, rotQuaternion, scale);
                    else UnityEngine.Gizmos.DrawWireMesh(mesh, corPosition, rotQuaternion, scale);
                    break;
                case SimpleGizmosType.Ray:
                    UnityEngine.Gizmos.DrawRay(corPosition, direction);
                    break;
                case SimpleGizmosType.Sphere:
                    UnityEngine.Gizmos.DrawSphere(corPosition, radius);
                    break;
                case SimpleGizmosType.WireSphere:
                    UnityEngine.Gizmos.DrawWireSphere(corPosition, radius);
                    break;
            }
        }
    }

#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(SimpleGizmos))]
    public class SimpleGizmosInspector : Editor
    {

        class SGDataEntry
        {
            public class SGProperty
            {
                public string propertyName;
                public SerializedProperty property;

                public SGProperty(string propName)
                {
                    propertyName = propName;
                }
            }

            public SimpleGizmosType[] types;

            public SGProperty[] properties;

            public SGDataEntry(SimpleGizmosType type, params string[] properties)
            {
                this.types = new[] { type };
                this.properties = new SGProperty[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    this.properties[i] = new SGProperty(properties[i]);
                }
            }
            public SGDataEntry(SimpleGizmosType[] types, params string[] properties)
            {
                this.types = types;
                this.properties = new SGProperty[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    this.properties[i] = new SGProperty(properties[i]);
                }
            }
        }

        static SGDataEntry[] data = new SGDataEntry[]
        {
            new SGDataEntry(new SimpleGizmosType[] { SimpleGizmosType.Cube, SimpleGizmosType.WireCube }, "position", "size"),
            new SGDataEntry(SimpleGizmosType.Frustum, "position", "rotation", "fov", "minRange", "maxRange", "aspect"),
            new SGDataEntry(SimpleGizmosType.GUITexture, "screen", "texture", "textureMaterial"),
            new SGDataEntry(SimpleGizmosType.Icon, "position", "iconname", "allowScaling"),
            new SGDataEntry(SimpleGizmosType.Line, "position", "to"),
            new SGDataEntry(new SimpleGizmosType[] {SimpleGizmosType.Mesh, SimpleGizmosType.WireMesh}, "mesh", "submeshIndex", "position", "rotation", "scale"),
            new SGDataEntry(SimpleGizmosType.Ray, "position", "direction"),
            new SGDataEntry(new SimpleGizmosType[] { SimpleGizmosType.Sphere, SimpleGizmosType.WireSphere}, "position", "radius")
        };

        private SimpleGizmos obj;

        private SGDataEntry currentSelected;
        private SerializedProperty renderType;
        private SerializedProperty color;
        private SerializedProperty transformPos;

        private void OnEnable()
        {
            obj = (SimpleGizmos)serializedObject.targetObject;
            currentSelected = GetConnected(obj.renderType);

            renderType = serializedObject.FindProperty("renderType");
            color = serializedObject.FindProperty("color");
            transformPos = serializedObject.FindProperty("useTransform");
        
            foreach(SGDataEntry item in data)
            {
                foreach(SGDataEntry.SGProperty props in item.properties)
                {
                    props.property = serializedObject.FindProperty(props.propertyName);
                }
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SimpleGizmosType lastType = obj.renderType;
            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Base", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(renderType);
            EditorGUILayout.PropertyField(color);
            EditorGUILayout.PropertyField(transformPos);

            EditorGUILayout.Separator();
            EditorGUILayout.LabelField("Options", EditorStyles.boldLabel);
            foreach (SGDataEntry.SGProperty prop in currentSelected.properties)
            {
                if (obj.useTransform && (prop.propertyName == "position" || prop.propertyName == "rotation")) continue;
                EditorGUILayout.PropertyField(prop.property);

            }

            serializedObject.ApplyModifiedProperties();

            if (lastType != obj.renderType)
            {
                currentSelected = GetConnected(obj.renderType);
            }
        }

        private SGDataEntry GetConnected(SimpleGizmosType type)
        {
            return data.First(a => a.types.Contains(type));
        }
    }
#endif
}