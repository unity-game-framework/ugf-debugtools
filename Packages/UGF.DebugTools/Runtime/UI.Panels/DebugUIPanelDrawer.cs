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

        public void Add(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            m_panels.Add(panel);

            panel.Enable();
        }

        public bool Remove(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            if (m_panels.Remove(panel))
            {
                panel.Disable();
                return true;
            }

            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < m_panels.Count; i++)
            {
                DebugUIPanel panel = m_panels[i];

                panel.Disable();
            }

            m_panels.Clear();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            m_functionDisplay = DebugUI.AddFunction(DebugUI.DebugFunctionGroupName, "Panels Display", OnFunctionDisplay);
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            DebugUI.RemoveFunction(DebugUI.DebugFunctionGroupName, m_functionDisplay);
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
