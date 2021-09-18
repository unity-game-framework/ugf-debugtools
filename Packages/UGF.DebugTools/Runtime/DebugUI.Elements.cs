using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static float LineHeight { get; } = 21F;
        public static float LineSpacing { get; } = 3F;

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

        public static Rect GetControlRect()
        {
            return GUILayoutUtility.GetRect(0F, float.MaxValue, LineHeight, LineHeight);
        }

        public static void Space()
        {
            GUILayout.Space(LineHeight);
        }
    }
}
