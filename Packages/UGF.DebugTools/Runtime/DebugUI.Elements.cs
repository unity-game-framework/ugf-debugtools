using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static bool Dropdown(GUIContent content, out Rect dropdownPosition)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            dropdownPosition = GUILayoutUtility.GetRect(content, GUI.skin.button);

            if (Event.current.type == EventType.Repaint)
            {
                GUI.skin.FindStyle(DebugUIStyles.Foldout).Draw(dropdownPosition, false, false, true, false);
            }

            return GUI.Button(dropdownPosition, content);
        }
    }
}
