using UGF.DebugTools.Runtime.UI.Panels;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static DebugUIPanelText PanelText(string text)
        {
            var panel = AddPanel<DebugUIPanelText>();

            panel.Text = text;

            return panel;
        }

        public static T AddPanel<T>() where T : DebugUIPanel, new()
        {
            var panel = new T();

            AddPanel(panel);

            return panel;
        }

        public static void AddPanel(DebugUIPanel panel)
        {
            Provider.Drawers.Get<DebugUIPanelDrawer>().Panels.Add(panel);

            panel.Initialize();
        }

        public static bool RemovePanel(DebugUIPanel panel)
        {
            panel.Uninitialize();

            return Provider.Drawers.Get<DebugUIPanelDrawer>().Panels.Remove(panel);
        }
    }
}
