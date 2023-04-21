using UnityEditor;
using UnityEngine;

namespace Common.Editor.EditorExtensions
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    [CanEditMultipleObjects]
    public class BaseEditor : ExtendedEditor
    {
        public override void OnInspectorGUI()
        {
        }
    }
}