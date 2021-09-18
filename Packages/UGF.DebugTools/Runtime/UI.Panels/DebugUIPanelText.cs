using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public class DebugUIPanelText : DebugUIPanel
    {
        public string Text { get; set; } = "Text";

        protected override void OnDrawGUILayout()
        {
            GUILayout.Label(Text);
        }
    }
}
