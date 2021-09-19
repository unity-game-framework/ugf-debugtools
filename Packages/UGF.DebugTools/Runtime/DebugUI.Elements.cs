using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static float LineHeight { get; set; } = 21F;
        public static float LineSpacing { get; set; } = 3F;
        public static int IndentLevel { get; set; }
        public static float IndentLevelSize { get; set; } = 21F;

        private static readonly int m_delayedTextFieldControlHint = nameof(DelayedTextField).GetHashCode();
        private static readonly string m_delayedTextFieldControlName = nameof(DelayedTextField);
        private static string m_delayedTextFieldValueControl;
        private static string m_delayedTextFieldValue;

        public static void Header(string content)
        {
            Header(DebugUIContentCache.GetContent(content));
        }

        public static void Header(GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            GUILayout.Label(content, DebugUIStyles.Header);
        }

        public static bool Foldout(string content, bool value)
        {
            return Foldout(DebugUIContentCache.GetContent(content), value);
        }

        public static bool Foldout(GUIContent content, bool value)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            return GUILayout.Toggle(value, content, DebugUIStyles.Foldout);
        }

        public static bool Dropdown(string content)
        {
            return Dropdown(DebugUIContentCache.GetContent(content));
        }

        public static bool Dropdown(GUIContent content)
        {
            return Dropdown(GetControlRect(), content);
        }

        public static bool Dropdown(Rect position, string content)
        {
            return Dropdown(position, DebugUIContentCache.GetContent(content));
        }

        public static bool Dropdown(Rect position, GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            bool value = GUI.Button(position, content, DebugUIStyles.ButtonTextLeft);

            if (Event.current.type == EventType.Repaint)
            {
                float padding = GUI.skin.button.padding.right;
                var rectFoldout = new Rect(position.xMax - position.height - padding, position.y, LineHeight, LineHeight);

                GUI.skin.FindStyle(DebugUIStyles.Foldout).Draw(rectFoldout, false, false, true, false);
            }

            return value;
        }

        public static void ValueEnum(string content, object value, DebugUIMenuItemHandler handler)
        {
            ValueEnum(DebugUIContentCache.GetContent(content), value, handler);
        }

        public static void ValueEnum(GUIContent content, object value, DebugUIMenuItemHandler handler)
        {
            ValueEnum(GetControlRect(), content, value, handler);
        }

        public static void ValueEnum(Rect position, string content, object value, DebugUIMenuItemHandler handler)
        {
            ValueEnum(position, DebugUIContentCache.GetContent(content), value, handler);
        }

        public static void ValueEnum(Rect position, GUIContent content, object value, DebugUIMenuItemHandler handler)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            if (Dropdown(position, content))
            {
                DebugUIMenu menu = GetMenuFromEnum(value, handler);

                menu.ShowDropdown(position);
            }
        }

        public static T Value<T>(Rect position, T value)
        {
            return (T)Value(position, value, typeof(T));
        }

        public static string DelayedTextField(string value)
        {
            return DelayedTextField(GetControlRect(), value);
        }

        public static string DelayedTextField(Rect position, string value)
        {
            int controlId = GUIUtility.GetControlID(m_delayedTextFieldControlHint, FocusType.Keyboard, position);
            string controlName = $"{m_delayedTextFieldControlName}:{controlId}";

            GUI.SetNextControlName(controlName);

            string controlFocused = GUI.GetNameOfFocusedControl();

            if (m_delayedTextFieldValueControl == null && controlName == controlFocused)
            {
                m_delayedTextFieldValueControl = controlName;
                m_delayedTextFieldValue = value;
            }

            if (m_delayedTextFieldValueControl == controlName && m_delayedTextFieldValueControl != controlFocused)
            {
                value = m_delayedTextFieldValue;

                m_delayedTextFieldValueControl = null;
                m_delayedTextFieldValue = null;
            }

            if (m_delayedTextFieldValueControl == controlName)
            {
                m_delayedTextFieldValue = GUI.TextField(position, m_delayedTextFieldValue);

                return value;
            }

            return GUI.TextField(position, value);
        }

        public static object Value(object value, Type type)
        {
            return Value(GetControlRect(), value, type);
        }

        public static object Value(Rect position, object value, Type type)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (type == null) throw new ArgumentNullException(nameof(type));

            string text = (string)Convert.ChangeType(value, typeof(string));

            text = DelayedTextField(position, text);

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

        public static void Spacing()
        {
            GUILayout.Space(LineSpacing);
        }
    }
}
