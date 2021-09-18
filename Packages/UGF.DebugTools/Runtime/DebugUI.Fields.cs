using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
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

        public static T FieldValue<T>(GUIContent label, T value)
        {
            return (T)FieldValue(label, value, typeof(T));
        }

        public static object FieldValue(GUIContent label, object value, Type type)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (type == null) throw new ArgumentNullException(nameof(type));

            Rect position = FieldPrefixLabel(label);

            return Value(position, value, type);
        }

        public static string FieldTextArea(GUIContent label, string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (label != GUIContent.none)
            {
                GUILayout.Label(label);
            }

            return GUILayout.TextArea(value);
        }

        public static string FieldText(GUIContent label, string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            Rect position = FieldPrefixLabel(label);

            return GUI.TextField(position, value);
        }

        public static string FieldPassword(GUIContent label, string value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            Rect position = FieldPrefixLabel(label);

            return GUI.PasswordField(position, value, '*');
        }

        public static bool FieldToggle(GUIContent label, bool value)
        {
            Rect position = FieldPrefixLabel(label);

            return GUI.Toggle(position, value, GUIContent.none);
        }

        public static bool FieldDropdown(GUIContent label, GUIContent content, DebugUIMenu menu)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            Rect position = FieldPrefixLabel(label);

            return MenuDropdown(position, content, menu);
        }

        public static bool FieldButton(GUIContent label, GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            Rect position = FieldPrefixLabel(label);

            return GUI.Button(position, content);
        }

        public static void FieldLabel(GUIContent label, GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            Rect position = FieldPrefixLabel(label);

            GUI.Label(position, content);
        }

        public static Rect FieldPrefixLabel(GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            FieldPositions positions = GetFieldPositions();

            FieldPrefixLabel(positions.Label, content);

            return content != GUIContent.none ? positions.Field : positions.Position;
        }

        public static void FieldPrefixLabel(Rect position, GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            if (content != GUIContent.none)
            {
                GUI.Label(position, content);
            }
        }

        public static FieldPositions GetFieldPositions()
        {
            Rect position = GetControlRect();

            return GetFieldPositions(position);
        }

        public static FieldPositions GetFieldPositions(Rect position)
        {
            var label = new Rect(position.x, position.y, position.width * FieldLabelRatio, position.height);
            var field = new Rect(label.xMax, position.y, position.width * (1F - FieldLabelRatio), position.height);

            label = GetIndentedRect(label);

            return new FieldPositions(position, label, field);
        }
    }
}
