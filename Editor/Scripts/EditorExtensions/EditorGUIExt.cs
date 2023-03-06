﻿using System;
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
        public static void HorizontalLine(Color color, float height) => HorizontalLine(color, height, DEFAULT_LINE_MARGIN);
        public static void HorizontalLine(Color color, Vector2 margin) => HorizontalLine(color, DEFAULT_LINE_HEIGHT, margin);
        public static void HorizontalLine(float height, Vector2 margin) => HorizontalLine(DEFAULT_COLOR, height, margin);

        public static void HorizontalLine(Color color) => HorizontalLine(color, DEFAULT_LINE_HEIGHT, DEFAULT_LINE_MARGIN);
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
        public static bool Foldout(bool value, GUIContent title, Action content, Action<Rect> menuAction = null, GUIStyle style = null, GUIStyle menuIcon = null)
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
            editor._foldouts[id] = Foldout(editor._foldouts.ContainsKey(id) && editor._foldouts[id], title, content, menuAction, style, menuIcon);
        }

        public static void Horizontal(Action<Rect> content, GUIStyle style = null, params GUILayoutOption[] options)
        {
            content(EditorGUILayout.BeginHorizontal(style, options));
            EditorGUILayout.EndHorizontal();
        }
        public static void Vertical(Action<Rect> content, GUIStyle style = null, params GUILayoutOption[] options)
        {
            content(EditorGUILayout.BeginVertical(style, options));
            EditorGUILayout.EndVertical();
        }

        public static Vector2 ScrollView(Vector2 scrollPosition, Action content, bool alwaysShowHorizontal = true,
            bool alwaysShowVertical = false, GUIStyle horizontalScrollbar = null, GUIStyle verticalScrollbar = null, GUIStyle background = null,
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

        public static bool CheckedObjectField(string title, Object obj, Type objType, Func<Object, bool> checkAction, out Object result)
        {
            EditorGUI.BeginChangeCheck();
            result = EditorGUILayout.ObjectField(title, obj, objType, false);
            bool changed = EditorGUI.EndChangeCheck();
            if (!changed) return false;

            if (result == null || !checkAction(result)) result = null;
            return true;
        }
    }
}