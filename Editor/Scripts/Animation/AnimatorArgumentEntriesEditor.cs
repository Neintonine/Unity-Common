using System.Collections.Generic;
using Common.Editor.EditorExtensions;
using Common.Runtime.Animation;
using UnityEditor;
using UnityEngine;

namespace Common.Editor.Animation
{
    [CustomEditor(typeof(AnimatorArgumentEntries))]
    public class AnimatorArgumentEntriesEditor : ExtendedEditor<AnimatorArgumentEntries>
    {
        public override void OnInspectorGUI()
        {
            GUILayoutOption buttonWidth = GUILayout.MaxWidth(30); 
            
            List<AnimatorArgumentDataEntry> list = Target.Entries;
            
            EditorGUILayout.Space();

            EditorGUIExt.Horizontal(rect =>
            {
                EditorGUILayout.LabelField("Values:", EditorStyles.boldLabel);
                if (GUILayout.Button("+", buttonWidth))
                {
                    list.Add(new AnimatorArgumentDataEntry("", null));
                }
            });
            EditorGUIExt.HorizontalLine(2);

            foreach (AnimatorArgumentDataEntry entry in list.ToArray())
            {
                EditorGUIExt.Horizontal(rect =>
                {
                    entry.Name = EditorGUILayout.TextField(entry.Name);
                    entry.Data = EditorGUILayout.ObjectField(entry.Data, typeof(Object), true);
                    
                    if (GUILayout.Button("-", buttonWidth))
                    {
                        list.Remove(entry);
                    }
                } );
            }
            
            base.OnInspectorGUI();
        }
    }
}