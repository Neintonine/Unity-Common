using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Common.Editor.EditorExtensions
{
    public static class EditorGUIExt
    {
        public static readonly Color DEFAULT_COLOR = new Color(0f, 0f, 0f, 0.3f);
        public static readonly Vector2 DEFAULT_LINE_MARGIN = new Vector2(2f, 1f);

        public const float DEFAULT_LINE_HEIGHT = 2f;

        public static void HeaderField(string name)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
            HorizontalLine(2);
        }

        public static void HeaderField(string name, float spaceWidth)
        {
            EditorGUILayout.Space(spaceWidth);
            EditorGUILayout.LabelField(name, EditorStyles.boldLabel);
            HorizontalLine(2);
        }

        #region HorizontalLine

        public static void HorizontalLine(Color color, float height, Vector2 margin)
        {
            GUILayout.Space(margin.x);

            EditorGUI.DrawRect(EditorGUILayout.GetControlRect(false, height), color);

            GUILayout.Space(margin.y);
        }

        public static void HorizontalLine(Color color, float height) =>
            HorizontalLine(color, height, DEFAULT_LINE_MARGIN);

        public static void HorizontalLine(Color color, Vector2 margin) =>
            HorizontalLine(color, DEFAULT_LINE_HEIGHT, margin);

        public static void HorizontalLine(float height, Vector2 margin) =>
            HorizontalLine(DEFAULT_COLOR, height, margin);

        public static void HorizontalLine(Color color) =>
            HorizontalLine(color, DEFAULT_LINE_HEIGHT, DEFAULT_LINE_MARGIN);

        public static void HorizontalLine(float height) => HorizontalLine(DEFAULT_COLOR, height, DEFAULT_LINE_MARGIN);
        public static void HorizontalLine(Vector2 margin) => HorizontalLine(DEFAULT_COLOR, DEFAULT_LINE_HEIGHT, margin);

        public static void HorizontalLine() => HorizontalLine(DEFAULT_COLOR, DEFAULT_LINE_HEIGHT, DEFAULT_LINE_MARGIN);

        #endregion

        public static void DisabledGroup(bool value, Action content)
        {
            EditorGUI.BeginDisabledGroup(value);
            content();
            EditorGUI.EndDisabledGroup();
        }

        public static bool ChangeCheck(Action content)
        {
            EditorGUI.BeginChangeCheck();
            content();
            return EditorGUI.EndChangeCheck();
        }

        public static void FadeGroup(float value, Action content)
        {
            EditorGUILayout.BeginFadeGroup(value);
            content();
            EditorGUILayout.EndFadeGroup();
        }

        public static bool Foldout(bool value, string title, Action content, Action<Rect> menuAction = null,
            GUIStyle style = null, GUIStyle menuIcon = null) =>
            Foldout(value, new GUIContent(title), content, menuAction, style, menuIcon);

        public static bool Foldout(bool value, GUIContent title, Action content, Action<Rect> menuAction = null,
            GUIStyle style = null, GUIStyle menuIcon = null)
        {
            bool changed = EditorGUILayout.BeginFoldoutHeaderGroup(value, title, style, menuAction, menuIcon);
            if (changed) content();
            EditorGUILayout.EndFoldoutHeaderGroup();
            return changed;
        }

        public static void Foldout(this ExtendedEditor editor, int id, string title, Action content,
            Action<Rect> menuAction = null, GUIStyle style = null, GUIStyle menuIcon = null) =>
            Foldout(editor, id, new GUIContent(title), content, menuAction, style, menuIcon);

        public static void Foldout(this ExtendedEditor editor, int id, GUIContent title, Action content,
            Action<Rect> menuAction = null, GUIStyle style = null, GUIStyle menuIcon = null)
        {
            editor._foldouts[id] = Foldout(editor._foldouts.ContainsKey(id) && editor._foldouts[id], title, content,
                menuAction, style, menuIcon);
        }

        public static void Horizontal(Action<Rect> content, GUIStyle style = null, params GUILayoutOption[] options)
        {
            content(EditorGUILayout.BeginHorizontal(style ?? GUIStyle.none, options));
            EditorGUILayout.EndHorizontal();
        }

        public static void Vertical(Action<Rect> content, GUIStyle style = null, params GUILayoutOption[] options)
        {
            content(EditorGUILayout.BeginVertical(style, options));
            EditorGUILayout.EndVertical();
        }

        public static Vector2 ScrollView(Vector2 scrollPosition, Action content, bool alwaysShowHorizontal = true,
            bool alwaysShowVertical = false, GUIStyle horizontalScrollbar = null, GUIStyle verticalScrollbar = null,
            GUIStyle background = null,
            params GUILayoutOption[] options)
        {
            Vector2 ret = EditorGUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical,
                horizontalScrollbar, verticalScrollbar, background, options);
            content();
            EditorGUILayout.EndScrollView();
            return ret;
        }

        public static bool ToggleGroup(string label, bool toggle, Action content) =>
            ToggleGroup(new GUIContent(label), toggle, content);

        public static bool ToggleGroup(GUIContent label, bool toggle, Action content)
        {
            bool ret = EditorGUILayout.BeginToggleGroup(label, toggle);
            content();
            EditorGUILayout.EndToggleGroup();
            return ret;
        }

        public static bool CheckedObjectField(string title, Object obj, Type objType, Func<Object, bool> checkAction,
            out Object result)
        {
            EditorGUI.BeginChangeCheck();
            result = EditorGUILayout.ObjectField(title, obj, objType, false);
            bool changed = EditorGUI.EndChangeCheck();
            if (!changed) return false;

            if (result == null || !checkAction(result)) result = null;
            return true;
        }

        public static object GenericObjectField(string label, object obj)
        {
            switch (obj)
            {
                case Bounds boundsVal:
                    return EditorGUILayout.BoundsField(label, boundsVal);
                case BoundsInt boundsIntVal:
                    return EditorGUILayout.BoundsIntField(label, boundsIntVal);
                case Color colorVal:
                    return EditorGUILayout.ColorField(label, colorVal);
                case AnimationCurve curveVal:
                    return EditorGUILayout.CurveField(label, curveVal);
                case double doubleVal:
                    return EditorGUILayout.DoubleField(label, doubleVal);
                case Enum enumVal:
                    return EditorGUILayout.EnumFlagsField(label, enumVal);
                case float floatValue:
                    return EditorGUILayout.FloatField(label, floatValue);
                case Gradient gradientVal:
                    return EditorGUILayout.GradientField(label, gradientVal);
                case int intVal:
                    return EditorGUILayout.IntField(label, intVal);
                case long longVal:
                    return EditorGUILayout.LongField(label, longVal);
                case SerializedProperty propertyVal:
                    EditorGUILayout.PropertyField(propertyVal);
                    break;
                case Rect rectVal:
                    return EditorGUILayout.RectField(label, rectVal);
                case RectInt rectIntVal:
                    return EditorGUILayout.RectIntField(label, rectIntVal);
                case bool boolVal:
                    return EditorGUILayout.Toggle(label, boolVal);
                case Vector2 vec2Val:
                    return EditorGUILayout.Vector2Field(label, vec2Val);
                case Vector2Int vec2Val:
                    return EditorGUILayout.Vector2IntField(label, vec2Val);
                case Vector3 vec3Val:
                    return EditorGUILayout.Vector3Field(label, vec3Val);
                case Vector3Int vec3Val:
                    return EditorGUILayout.Vector3IntField(label, vec3Val);
                case Vector4 vec4Val:
                    return EditorGUILayout.Vector4Field(label, vec4Val);

                case Object unityObj:
                    return EditorGUILayout.ObjectField(label, unityObj, unityObj.GetType(), true);
            }

            return obj;
        }
    }
}