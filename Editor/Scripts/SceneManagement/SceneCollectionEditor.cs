using Common.Editor;
using Common.Editor.EditorExtensions;
using Common.Runtime.SceneManagement;
using Common.Runtime.SceneManagement.Collections;
using SceneManagement.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneManagement.Editor
{
    [CustomEditor(typeof(SceneCollection))]
    public class SceneCollectionEditor : ExtendedEditor<SceneCollection>
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Load in Editor"))
            {
                LoadingManager.LoadSceneEditor(Target);
            }
            
            RenderProperty("_ignoreDefaultScenes", "_scenes");
            
            base.OnInspectorGUI();
        }
    }
}