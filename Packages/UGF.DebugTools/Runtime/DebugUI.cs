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

        public static bool PanelAdd(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            return Drawer.Panels.Add(panel);
        }

        public static bool PanelRemove(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            return Drawer.Panels.Remove(panel);
        }
    }
}
