using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public class DebugUIPanelDrawer : DebugUIDrawerBase
    {
        public IReadOnlyCollection<DebugUIPanel> Panels { get; }

        private readonly List<DebugUIPanel> m_panels = new List<DebugUIPanel>();

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

        protected override void OnDrawGUI()
        {
            for (int i = 0; i < m_panels.Count; i++)
            {
                DebugUIPanel panel = m_panels[i];

                panel.DrawGUI();
            }
        }
    }
}
