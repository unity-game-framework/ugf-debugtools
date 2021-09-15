using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUI
    {
        public static DebugUIDrawer Drawer { get; } = new DebugUIDrawer();

        private static readonly DebugUIMenuDrawer m_menu = new DebugUIMenuDrawer();

        static DebugUI()
        {
            DebugUISettingsAsset settings = DebugUISettings.Settings.GetData();

            Drawer.Scale = settings.Scale;

            if (settings.Skin != null)
            {
                Drawer.SetSkin(settings.Skin);
            }

            for (int i = 0; i < settings.Drawers.Count; i++)
            {
                AssetReference<DebugUIDrawerAsset> reference = settings.Drawers[i];
                IDebugUIDrawer drawer = reference.Asset.Build();

                Drawer.AddDrawer(reference.Guid, drawer);
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

        public static void MenuShow(DebugUIMenu menu, Rect position)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            m_menu.Position = position;
            m_menu.SetMenu(menu);
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

            if (m_menu.AnySelected())
            {
                m_menu.ClearMenu();
            }
        }
    }
}
