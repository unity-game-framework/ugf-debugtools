using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuItemContent : DebugUIMenuItem
    {
        public bool Enabled { get; }
        public GUIContent Content { get; }

        public DebugUIMenuItemContent(GUIContent content, bool enabled, DebugUIMenuItemHandler handler) : base(handler)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Enabled = enabled;
        }

        protected override void OnDrawGUILayout()
        {
            Rect position = GUILayoutUtility.GetRect(Content, DebugUIStyles.FrameHighlight);

            if (Event.current.type == EventType.Repaint)
            {
                GUI.skin.FindStyle(DebugUIStyles.CheckMark).Draw(position, false, false, Enabled, false);
            }

            if (GUI.Button(position, Content, DebugUIStyles.FrameHighlight))
            {
                Select();
            }
        }
    }
}
