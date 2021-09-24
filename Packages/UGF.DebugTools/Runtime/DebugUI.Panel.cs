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
            Drawer.Get<DebugUIPanelDrawer>().Add(panel);

            panel.Initialize();
        }

        public static bool PanelRemove(DebugUIPanel panel)
        {
            panel.Uninitialize();

            return Drawer.Get<DebugUIPanelDrawer>().Remove(panel);
        }
    }
}
