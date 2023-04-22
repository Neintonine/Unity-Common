using Common.Editor.EditorExtensions;
using Common.Runtime.Animation;
using UnityEditor;

namespace Common.Editor.Animation
{
    [CustomEditor(typeof(AnimatorArgumentsTransform))]
    public class AnimatorArgumentsTransformEditor : ExtendedEditor<AnimatorArgumentsTransform>
    {
        public override void OnInspectorGUI()
        {
            this.RenderProperty("_transform");
            
            EditorGUIExt.HeaderField("Values");

            this.RenderTransformOptions(
                "Position",
                "_addPosition",
                "_positionName",
                useWorld: "_useWorldPosition"
            );
            this.RenderTransformOptions(
                "Rotation",
                "_addRotation",
                "_rotationName",
                useWorld: "_useWorldRotation",
                useEuler: "_useEuler"
            );
            this.RenderTransformOptions(
                "Scale",
                "_addScale",
                "_scaleName"
            );
            
            EditorGUIExt.HeaderField("Directions");
            this.RenderTransformOptions(
                "Forward",
                "_addForward",
                "_forwardName"
            );
            this.RenderTransformOptions(
                "Right",
                "_addRight",
                "_rightName"
            );
            this.RenderTransformOptions(
                "Up",
                "_addUp",
                "_upName"
            );

            base.OnInspectorGUI();
        }

        private void RenderTransformOptions(
            string label,
            string add,
            string objectName,
            string useWorld = "", 
            string useEuler = ""
        )
        {
            SerializedProperty addProp = this.GetProperty(add);
            SerializedProperty nameProp = this.GetProperty(objectName);

            addProp.boolValue = EditorGUILayout.ToggleLeft(label, addProp.boolValue);
            if (!addProp.boolValue)
            {
                return;
            }
            EditorGUI.indentLevel = 2;

            nameProp.stringValue = EditorGUILayout.TextField(nameProp.stringValue);
            
            bool hasUseWorld = !string.IsNullOrEmpty(useWorld);
            bool hasUseEuler = !string.IsNullOrEmpty(useEuler);
            EditorGUIExt.Horizontal(_ =>
            {
                if (hasUseEuler)
                {
                    SerializedProperty eulerProp = this.GetProperty(useEuler);
                    eulerProp.boolValue = EditorGUILayout.ToggleLeft("Use Euler", eulerProp.boolValue);
                }
                
                if (hasUseWorld)
                {
                    SerializedProperty worldProp = this.GetProperty(useWorld);
                    worldProp.boolValue = EditorGUILayout.ToggleLeft("Use World", worldProp.boolValue);
                }
            });
            EditorGUI.indentLevel = 0;

        }
    }
}