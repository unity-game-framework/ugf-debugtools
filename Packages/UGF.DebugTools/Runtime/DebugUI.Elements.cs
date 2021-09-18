﻿using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static float LineHeight { get; set; } = 21F;
        public static float LineSpacing { get; set; } = 3F;
        public static int IndentLevel { get; set; }
        public static float IndentLevelSize { get; set; } = 21F;

        public static void Header(GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            GUILayout.Label(content, DebugUIStyles.Header);
        }

        public static bool Foldout(GUIContent content, bool value)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            return GUILayout.Toggle(value, content, DebugUIStyles.Foldout);
        }

        public static bool Dropdown(Rect position, GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            bool value = GUI.Button(position, content);

            if (Event.current.type == EventType.Repaint)
            {
                float padding = GUI.skin.button.padding.right;
                var rectFoldout = new Rect(position.xMax - position.height - padding, position.y, LineHeight, LineHeight);

                GUI.skin.FindStyle(DebugUIStyles.Foldout).Draw(rectFoldout, false, false, true, false);
            }

            return value;
        }

        public static T Value<T>(Rect position, T value)
        {
            return (T)Value(position, value, typeof(T));
        }

        public static object Value(Rect position, object value, Type type)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (type == null) throw new ArgumentNullException(nameof(type));

            string text = (string)Convert.ChangeType(value, typeof(string));

            text = GUI.TextField(position, text);

            try
            {
                value = Convert.ChangeType(text, type);
            }
            catch
            {
                // ignored
            }

            return value;
        }

        public static Rect GetControlRect()
        {
            return GUILayoutUtility.GetRect(0F, float.MaxValue, LineHeight, LineHeight);
        }

        public static Rect GetIndentedRect(Rect rect)
        {
            float indent = GetIndentWidth();

            rect.xMin += indent;
            rect.width -= indent;

            return rect;
        }

        public static float GetIndentWidth()
        {
            return GetIndentWidth(IndentLevel);
        }

        public static float GetIndentWidth(int level)
        {
            return IndentLevelSize * level;
        }

        public static void Space()
        {
            GUILayout.Space(LineHeight);
        }
    }
}
