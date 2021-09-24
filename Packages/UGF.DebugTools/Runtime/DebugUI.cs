using System;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static DebugUIDrawer Drawer { get { return m_drawer ?? throw new ArgumentException("Value not specified."); } }
        public static bool HasDrawer { get { return m_drawer != null; } }

        private static DebugUIDrawer m_drawer;

        public static void DrawerSet(DebugUIDrawer drawer)
        {
            m_drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
        }

        public static void DrawerClear()
        {
            m_drawer = null;
        }
    }
}
