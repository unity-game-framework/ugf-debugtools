using System.Collections.Generic;
using UGF.DebugTools.Runtime.UI.Functions;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public class DebugUIPanelDrawer : DebugUIDrawer
    {
        public List<DebugUIPanel> Panels { get; } = new List<DebugUIPanel>();
        public bool Display { get; set; }

        private DebugUIFunction m_functionDisplay;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            for (int i = 0; i < Panels.Count; i++)
            {
                Panels[i].Initialize();
            }

            m_functionDisplay = DebugUI.AddFunction(DebugUI.DebugFunctionGroupName, "Panels Display", OnFunctionDisplay);
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            for (int i = 0; i < Panels.Count; i++)
            {
                Panels[i].Uninitialize();
            }

            DebugUI.RemoveFunction(DebugUI.DebugFunctionGroupName, m_functionDisplay);
        }

        public void Clear()
        {
            for (int i = 0; i < Panels.Count; i++)
            {
                Panels[i].Uninitialize();
            }

            Panels.Clear();
        }

        protected override void OnDrawGUI()
        {
            if (Display)
            {
                for (int i = 0; i < Panels.Count; i++)
                {
                    DebugUIPanel panel = Panels[i];

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
