using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public class DebugUISectionOptions : DebugUISection
    {
        private readonly DebugUIMenuItemHandler m_onAlignmentSelect;
        private DebugUISectionDrawer m_drawer;

        public DebugUISectionOptions() : base("UI Section Options")
        {
            m_onAlignmentSelect = OnAlignmentSelect;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (DebugUI.Provider.Drawers.TryGet(out DebugUISectionDrawer drawer))
            {
                m_drawer = drawer;
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            m_drawer = null;
        }

        protected override void OnDrawGUILayout()
        {
            if (m_drawer != null)
            {
                Vector2 size = m_drawer.Size;

                size.x = DebugUI.FieldValue("Width", size.x);
                size.y = DebugUI.FieldValue("Height", size.y);

                m_drawer.Size = size;

                DebugUI.FieldEnum("Alignment", m_drawer.Alignment, m_onAlignmentSelect);
            }
            else
            {
                using (new DebugUICenterScope(GUIContent.none))
                {
                    GUILayout.Label("Debug UI Section Drawer not found.");
                }
            }
        }

        private void OnAlignmentSelect(DebugUIMenuItem item)
        {
            if (m_drawer != null)
            {
                m_drawer.Alignment = item.GetValue<DebugUISectionAlignment>();
            }
        }
    }
}
