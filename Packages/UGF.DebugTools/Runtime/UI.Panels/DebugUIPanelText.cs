using UGF.DebugTools.Runtime.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public class DebugUIPanelText : DebugUIPanel
    {
        public string Text { get; set; } = "Text";
        public bool DisplayBackground { get; set; } = false;

        protected override void OnDrawGUILayout()
        {
            if (DisplayBackground)
            {
                using (new DebugUIVerticalScope(GUIContent.none, GUI.skin.box))
                {
                    GUILayout.Label(Text);
                }
            }
            else
            {
                GUILayout.Label(Text);
            }
        }
    }
}
