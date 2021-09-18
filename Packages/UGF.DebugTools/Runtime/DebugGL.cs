using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugGL
    {
        public static DebugGLDrawer Drawer { get; } = new DebugGLDrawer();

        static DebugGL()
        {
            DebugGLSettingsAsset settings = DebugGLSettings.Settings.GetData();

            if (settings.Material != null)
            {
                Drawer.SetMaterial(settings.Material);
            }

            Camera.onPostRender += OnDrawGL;
        }

        private static void OnDrawGL(Camera camera)
        {
            Drawer.DrawGL();
        }

        [RuntimeInitializeOnLoadMethod]
        private static void OnInitialize()
        {
        }
    }
}
