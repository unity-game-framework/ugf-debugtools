using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    public class DebugUIStatsSection : DebugUISectionDescribed<DebugUIStatsSectionDescription>
    {
        public DebugUIStatsPanel Panel { get { return m_panel ?? throw new ArgumentException("Value not specified."); } }
        public bool HasPanel { get { return m_panel != null; } }

        private DebugUIStatsPanel m_panel;

        public DebugUIStatsSection(DebugUIStatsSectionDescription description) : base(description, "UI Stats")
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_panel = DebugUI.AddPanel<DebugUIStatsPanel>();

            m_panel.ScrollEnabled = false;
            m_panel.WindowType = DebugUIWindowType.Area;
            m_panel.SetSize(new Vector2(DebugUI.LineHeight * 2F, DebugUI.LineHeight));

            m_panel.Alignment = Description.Alignment;
            m_panel.Display = Description.Display;
            m_panel.DisplayBackground = Description.DisplayBackground;
            m_panel.Enabled = Description.DisplayEnabled;
            m_panel.DisplayFps = Description.DisplayFps;
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            DebugUI.RemovePanel(m_panel);

            m_panel = null;
        }

        protected override void OnDrawGUILayout()
        {
            DebugUI.FieldEnum("Alignment", Panel.Alignment, OnAlignmentChanged);

            Panel.Display = DebugUI.FieldToggle("Display", Panel.Display);
            Panel.DisplayBackground = DebugUI.FieldToggle("Display Background", Panel.DisplayBackground);
            Panel.Enabled = DebugUI.FieldToggle("Display Enabled", Panel.Enabled);
            Panel.DisplayFps = DebugUI.FieldToggle("Display FPS", Panel.DisplayFps);
        }

        private void OnAlignmentChanged(DebugUIMenuItem item)
        {
            Panel.Alignment = item.GetValue<DebugUIStatsAlignment>();
        }
    }
}
