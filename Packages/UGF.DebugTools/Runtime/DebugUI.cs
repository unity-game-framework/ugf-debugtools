using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static DebugUIDrawer Drawer { get; } = new DebugUIDrawer();

        static DebugUI()
        {
            DebugUISettingsAsset settings = DebugUISettings.Settings.GetData();

            Drawer.Scale = Vector2.one * settings.Scale;

            if (settings.Skin != null)
            {
                Drawer.SetSkin(settings.Skin);
            }

            for (int i = 0; i < settings.Drawers.Count; i++)
            {
                AssetReference<DebugUIDrawerAsset> reference = settings.Drawers[i];
                IDebugUIDrawer drawer = reference.Asset.Build();

                Drawer.Add(reference.Guid, drawer);
            }

            OnCreateExecuter();
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

            DebugUIContentCache.Reset();
        }
    }
}
