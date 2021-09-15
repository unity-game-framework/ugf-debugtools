using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUI
    {
        public static DebugUIDrawer Drawer { get; } = new DebugUIDrawer();
        public static DebugUIExecutor Executor { get; }

        static DebugUI()
        {
            DebugSettingsAsset settings = DebugSettings.Settings.GetData();

            Drawer.Scale = settings.UIScale;

            if (settings.UISkin != null)
            {
                Drawer.SetSkin(settings.UISkin);
            }

            Executor = new GameObject(nameof(DebugUIExecutor)).AddComponent<DebugUIExecutor>();
            Executor.SetDrawer(Drawer);

            Object.DontDestroyOnLoad(Executor.gameObject);
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
            Drawer.AddPanel(panel);
        }

        public static bool PanelRemove(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            return Drawer.RemovePanel(panel);
        }
    }
}
