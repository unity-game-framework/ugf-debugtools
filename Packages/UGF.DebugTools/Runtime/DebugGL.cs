using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugGL
    {
        public static DebugGLDrawer Drawer { get; } = new DebugGLDrawer();

        static DebugGL()
        {
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
