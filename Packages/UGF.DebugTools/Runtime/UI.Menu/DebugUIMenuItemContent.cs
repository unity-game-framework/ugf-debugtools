using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuItemContent : DebugUIMenuItem
    {
        public bool Enabled { get; }
        public GUIContent Content { get; }

        public DebugUIMenuItemContent(GUIContent content, bool enabled, DebugUIMenuItemHandler handler, object value = null) : base(handler, value)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Enabled = enabled;
        }

        protected override void OnDrawGUILayout()
        {
            GUIStyle style = GUI.skin.FindStyle(DebugUIStyles.CheckMark);
            Rect position = GUILayoutUtility.GetRect(Content, DebugUIStyles.FrameHighlight);

            if (Event.current.type == EventType.Repaint && Enabled)
            {
                style.Draw(position, false, false, false, false);
            }

            var rectLabel = new Rect(position.x + style.normal.background.width, position.y, position.width - style.normal.background.width, position.height);

            GUI.Label(rectLabel, Content);

            if (GUI.Button(position, GUIContent.none, DebugUIStyles.FrameHighlight))
            {
                Select();
            }
        }
    }
}
