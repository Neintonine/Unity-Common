using Common.Editor;
using Common.Editor.EditorExtensions;
using Common.Runtime.SceneManagement;
using Common.Runtime.SceneManagement.Collections;
using SceneManagement.Runtime;
using UnityEditor;
using UnityEngine;

namespace SceneManagement.Editor
{
    [CustomEditor(typeof(LoadingManager))]
    public class LoadingManagerEditor : ExtendedEditor<LoadingManager>
    {
        private UnityEditor.Editor _globalSceneCollectionEditor;

        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (GlobalSceneCollection.IsAvailable)
            {
                _globalSceneCollectionEditor = UnityEditor.Editor.CreateEditor(GlobalSceneCollection.Instance);
            }
        }

        public override void OnInspectorGUI()
        {
            RenderProperty("_loadingScreen");

            EditorGUIExt.HeaderField("Global Settings");
            if (!GlobalSceneCollection.IsAvailable)
            {
                if (GUILayout.Button("Create Global Scene Collection"))
                {
                    GlobalSceneCollection.CreateAsset();
                    _globalSceneCollectionEditor = UnityEditor.Editor.CreateEditor(GlobalSceneCollection.Instance);

                }
                base.OnInspectorGUI();
                return;
            }
            
            _globalSceneCollectionEditor.OnInspectorGUI();
            
            base.OnInspectorGUI();
        }
    }
}