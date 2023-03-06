using System.Collections.Generic;
using UnityEditor;

namespace Common.Editor.EditorExtensions
{
    public abstract class ExtendedEditor : UnityEditor.Editor
    {
        internal Dictionary<int, bool> _foldouts = new Dictionary<int, bool>(); 
        private Dictionary<string, SerializedProperty> properties = new Dictionary<string, SerializedProperty>();

        protected virtual void OnEnable()
        {
            properties.Clear();
        }

        public override void OnInspectorGUI()
        {
            if (!serializedObject.hasModifiedProperties) return;

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
        }

        protected SerializedProperty GetProperty(string name)
        {
            if (properties.ContainsKey(name)) return properties[name];

            SerializedProperty property;
            properties.Add(name, property = serializedObject.FindProperty(name));
            return property;
        }

        protected void RenderProperty(string name)
        {
            EditorGUILayout.PropertyField(GetProperty(name));
        }

        protected void RenderProperty(params string[] names)
        {
            foreach (string s in names)
            {
                RenderProperty(s);
            }
        }
    }

    public abstract class ExtendedEditor<T> : ExtendedEditor
        where T : UnityEngine.Object
    {
        protected T Target => (T) this.target;
    }
}
