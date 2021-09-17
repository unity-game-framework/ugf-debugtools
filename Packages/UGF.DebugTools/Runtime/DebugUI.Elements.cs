using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static float LineHeight { get; } = 21F;
        public static float LineSpacing { get; } = 3F;
        public static float FieldLabelRatio { get; } = 0.4F;

        public readonly struct FieldPositions
        {
            public Rect Position { get; }
            public Rect Label { get; }
            public Rect Field { get; }

            public FieldPositions(Rect position, Rect label, Rect field)
            {
                Position = position;
                Label = label;
                Field = field;
            }
        }

        public static bool FieldDropdown(GUIContent label, GUIContent content, DebugUIMenu menu)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            FieldPositions positions = GetFieldPositions();

            FieldPrefixLabel(positions.Label, label);

            return MenuDropdown(positions.Field, content, menu);
        }

        public static bool FieldButton(GUIContent label, GUIContent content)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));

            FieldPositions positions = GetFieldPositions();

            FieldPrefixLabel(positions.Label, label);

            return GUI.Button(positions.Field, content);
        }

        public static void FieldLabel(GUIContent label, GUIContent content)
        {
            if (label == null) throw new ArgumentNullException(nameof(label));
            if (content == null) throw new ArgumentNullException(nameof(content));

            FieldPositions positions = GetFieldPositions();

            FieldPrefixLabel(positions.Label, label);

            GUI.Label(positions.Field, content);
        }

        public static void FieldPrefixLabel(Rect position, GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (content != GUIContent.none)
            {
                GUI.Label(position, content);
            }
        }

        public static bool Dropdown(Rect position, GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            bool value = GUI.Button(position, content);

            if (Event.current.type == EventType.Repaint)
            {
                var rectFoldout = new Rect(position.xMax - position.height - LineSpacing, position.y, LineHeight, LineHeight);

                GUI.skin.FindStyle(DebugUIStyles.Foldout).Draw(rectFoldout, false, false, true, false);
            }

            return value;
        }

        public static FieldPositions GetFieldPositions()
        {
            Rect position = GetControlRect();
            var label = new Rect(position.x, position.y, position.width * FieldLabelRatio, position.height);
            var field = new Rect(label.xMax, position.y, position.width * (1F - FieldLabelRatio), position.height);

            return new FieldPositions(position, label, field);
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
