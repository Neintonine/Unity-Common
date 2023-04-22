using Common.Editor.EditorExtensions;
using Common.Runtime.Animation;
using UnityEditor;
using Application = UnityEngine.Application;

namespace Common.Editor.Animation
{
    [CustomEditor(typeof(AnimatorArguments))]
    public class AnimatorArgumentsEditor : ExtendedEditor<AnimatorArguments>
    {
        public override void OnInspectorGUI()
        {
            if (!Application.isPlaying)
            {
                EditorGUILayout.LabelField("In edit mode, this will display currently set values.");
                return;
            }
            
            EditorGUIExt.HeaderField("Values contained:");
            
            EditorGUI.BeginDisabledGroup(true);
            foreach (AnimatorArgumentData entry in this.Target.GetAllValues())
            {
                EditorGUIExt.GenericObjectField(entry.Name, entry.Object);
            }
            EditorGUI.EndDisabledGroup();
            
        }
    }
}