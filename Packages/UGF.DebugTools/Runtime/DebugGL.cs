using System;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugGL
    {
        public static DebugGLDrawer Drawer { get { return m_drawer ?? throw new ArgumentException("Value not specified."); } }
        public static bool HasDrawer { get { return m_drawer != null; } }

        private static DebugGLDrawer m_drawer;

        public static void DrawerSet(DebugGLDrawer drawer)
        {
            m_drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
        }

        public static void DrawerClear()
        {
            m_drawer = null;
        }
    }
}
