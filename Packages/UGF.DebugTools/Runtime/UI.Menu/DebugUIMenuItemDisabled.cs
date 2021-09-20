using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuItemDisabled : DebugUIMenuItemContent
    {
        public DebugUIMenuItemDisabled(GUIContent content, bool enabled) : base(content, enabled, item => { })
        {
        }

        protected override void OnDrawGUILayout()
        {
            using (new DebugUIEnabledScope(false))
            {
                base.OnDrawGUILayout();
            }
        }
    }
}
