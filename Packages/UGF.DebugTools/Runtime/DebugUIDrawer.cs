using System.Collections.Generic;

namespace UGF.DebugTools.Runtime
{
    public class DebugUIDrawer
    {
        public HashSet<DebugUIPanel> Panels { get; } = new HashSet<DebugUIPanel>();

        public void DrawGUI()
        {
            foreach (DebugUIPanel panel in Panels)
            {
                panel.DrawGUI();
            }
        }
    }
}
