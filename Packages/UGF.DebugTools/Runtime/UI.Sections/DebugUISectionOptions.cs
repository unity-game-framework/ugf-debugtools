using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public class DebugUISectionOptions : DebugUISection
    {
        private readonly DebugUIMenuItemHandler m_onAlignmentSelect;
        private DebugUISectionDrawer m_drawer;
        private Vector2 m_size;

        public DebugUISectionOptions() : base("UI Section Options")
        {
            m_onAlignmentSelect = OnAlignmentSelect;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            if (DebugUI.Drawer.TryGet(out DebugUISectionDrawer drawer))
            {
                m_drawer = drawer;
                m_size = drawer.Size;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            m_drawer = null;
        }

        protected override void OnDrawGUILayout()
        {
            if (m_drawer != null)
            {
                m_size.x = DebugUI.FieldValue("Width", m_size.x);
                m_size.y = DebugUI.FieldValue("Height", m_size.y);

                DebugUI.FieldEnum("Alignment", m_drawer.Alignment, m_onAlignmentSelect);

                using (new DebugUIHorizontalScope(GUIContent.none))
                {
                    GUILayout.FlexibleSpace();

                    if (GUILayout.Button("Apply", GUILayout.Width(50F)))
                    {
                        m_drawer.Size = m_size;
                    }

                    DebugUI.Spacing();
                }
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
