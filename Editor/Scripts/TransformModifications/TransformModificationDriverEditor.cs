using Common.Editor.EditorExtensions;
using Common.Runtime.TransformModifications;
using UnityEditor;

namespace Common.Editor.TransformModifications
{
    [CustomEditor(typeof(TransformModificationDriver))]
    public class TransformModificationDriverEditor : ExtendedEditor<TransformModificationDriver>
    {
        public override void OnInspectorGUI()
        {
            EditorGUIExt.HeaderField("Original Values");
            EditorGUILayout.Vector3Field("Position", Target.OriginalPosition);
            EditorGUILayout.Vector3Field("Scale", Target.OriginalScale);
            EditorGUILayout.Vector3Field("Rotation", Target.OriginalRotation.eulerAngles);
            
            EditorGUIExt.HeaderField("Delta Values");
            EditorGUILayout.Vector3Field("Position", Target.DeltaPosition);
            EditorGUILayout.Vector3Field("Scale", Target.DeltaScale);
            EditorGUILayout.Vector3Field("Rotation", Target.DeltaRotation.eulerAngles);
            
            base.OnInspectorGUI();
        }
    }
}