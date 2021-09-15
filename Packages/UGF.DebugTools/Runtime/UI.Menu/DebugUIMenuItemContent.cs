using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuItemContent : DebugUIMenuItem
    {
        public GUIContent Content { get; }

        public DebugUIMenuItemContent(GUIContent content, DebugUIMenuItemHandler handler) : base(handler)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        protected override void OnDrawGUILayout()
        {
            if (GUILayout.Button(Content))
            {
                Select();
            }
        }
    }
}
