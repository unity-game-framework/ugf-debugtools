using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static void MenuShow(DebugUIMenu menu, Rect position)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            DebugUIMenuDrawer drawer = GetMenuDrawer();

            if (drawer.HasMenu)
            {
                drawer.ClearMenu();
            }

            drawer.Position = position;
            drawer.SetMenu(menu);
        }

        private static DebugUIMenuDrawer GetMenuDrawer()
        {
            if (!Drawer.TryGet(out DebugUIMenuDrawer drawer))
            {
                string id = Guid.NewGuid().ToString("N");

                drawer = new DebugUIMenuDrawer();

                Drawer.Add(id, drawer);
            }

            return drawer;
        }
    }
}
