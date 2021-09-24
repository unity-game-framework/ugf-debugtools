using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.UI.Functions;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public class DebugUIPanelDrawer : DebugUIDrawerBase
    {
        public bool Display { get; set; }
        public IReadOnlyCollection<DebugUIPanel> Panels { get; }

        private readonly List<DebugUIPanel> m_panels = new List<DebugUIPanel>();
        private DebugUIFunction m_functionDisplay;

        public DebugUIPanelDrawer()
        {
            Panels = new ReadOnlyCollection<DebugUIPanel>(m_panels);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            for (int i = 0; i < m_panels.Count; i++)
            {
                m_panels[i].Initialize();
            }

            m_functionDisplay = DebugUI.AddFunction(DebugUI.DebugFunctionGroupName, "Panels Display", OnFunctionDisplay);
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            for (int i = 0; i < m_panels.Count; i++)
            {
                m_panels[i].Uninitialize();
            }

            DebugUI.RemoveFunction(DebugUI.DebugFunctionGroupName, m_functionDisplay);
        }

        public void Add(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            m_panels.Add(panel);
        }

        public bool Remove(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            return m_panels.Remove(panel);
        }

        public void Clear()
        {
            for (int i = 0; i < m_panels.Count; i++)
            {
                m_panels[i].Uninitialize();
            }

            m_panels.Clear();
        }

        protected override void OnDrawGUI()
        {
            if (Display)
            {
                for (int i = 0; i < m_panels.Count; i++)
                {
                    DebugUIPanel panel = m_panels[i];

                    panel.DrawGUI();
                }
            }
        }

        private void OnFunctionDisplay(DebugUIFunction function)
        {
            Display = !Display;

            function.Enabled = Display;
        }
    }
}
