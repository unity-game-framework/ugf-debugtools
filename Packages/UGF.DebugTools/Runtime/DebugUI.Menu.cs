using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static void MenuShowDropdown(DebugUIMenu menu, Rect dropdownPosition)
        {
            Vector2 positionScreen = DebugUIUtility.GUIToScreenPosition(dropdownPosition.position);
            Rect position = DebugUIMenuUtility.GetMenuScreenPosition(new Vector2(positionScreen.x, positionScreen.y + dropdownPosition.height), dropdownPosition.width);

            MenuShow(menu, position);
        }

        public static void MenuShowContext(DebugUIMenu menu, float width = 150F)
        {
            Vector2 positionMouse = Event.current.mousePosition;
            Vector2 positionScreen = DebugUIUtility.GUIToScreenPosition(positionMouse);
            Rect position = DebugUIMenuUtility.GetMenuScreenPosition(positionScreen, width);

            MenuShow(menu, position);
        }

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
