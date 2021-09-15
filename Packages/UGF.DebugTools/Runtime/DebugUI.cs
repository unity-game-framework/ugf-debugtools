using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUI
    {
        public static DebugUIDrawer Drawer { get; } = new DebugUIDrawer();

        static DebugUI()
        {
            DebugSettingsAsset settings = DebugSettings.Settings.GetData();

            Drawer.Scale = settings.UIScale;

            if (settings.UISkin != null)
            {
                Drawer.SetSkin(settings.UISkin);
            }

            OnCreateExecuter();
        }

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

        private static void OnCreateExecuter()
        {
            var executer = new GameObject(nameof(DebugUIExecuter)).AddComponent<DebugUIExecuter>();

            executer.Drawing += OnDrawing;

            Object.DontDestroyOnLoad(executer.gameObject);
        }

        private static void OnDrawing()
        {
            Drawer.DrawGUI();
        }
    }
}
