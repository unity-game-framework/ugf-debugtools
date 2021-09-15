using UGF.DebugTools.Runtime.UI.Panels;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static DebugUIPanelText PanelText(string text)
        {
            var panel = PanelAdd<DebugUIPanelText>();

            panel.Text = text;

            return panel;
        }

        public static T PanelAdd<T>() where T : DebugUIPanel, new()
        {
            var panel = new T();

            PanelAdd(panel);

            return panel;
        }

        public static void PanelAdd(DebugUIPanel panel)
        {
            Drawer.Get<DebugUIPanelDrawer>().AddPanel(panel);
        }

        public static bool PanelRemove(DebugUIPanel panel)
        {
            return Drawer.Get<DebugUIPanelDrawer>().RemovePanel(panel);
        }
    }
}
