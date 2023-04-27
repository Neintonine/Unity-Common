using Common.Editor;
using Common.Editor.EditorExtensions;
using Common.Runtime.SceneManagement.Collections;
using SceneManagement.Runtime;
using UnityEditor;

namespace SceneManagement.Editor
{
    [CustomEditor(typeof(GlobalSceneCollection))]
    public class GlobalSceneCollectionEditor : ExtendedEditor<GlobalSceneCollection>
    {
        public override void OnInspectorGUI()
        {
            RenderProperty("_persistentScene", "_startCollection", "_defaultScenes");
            
            base.OnInspectorGUI();
        }
    }
}