using System;
using UGF.DebugTools.Runtime.Scopes;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static void MenuShowDropdown(DebugUIMenu menu, Rect dropdownPosition, float height = 300F)
        {
            using (new DebugUIMatrixScope(Matrix4x4.identity))
            {
                Vector2 screenPoint = GUIUtility.GUIToScreenPoint(dropdownPosition.position);
                var position = new Rect(screenPoint.x, screenPoint.y + dropdownPosition.height, dropdownPosition.width, height);

                MenuShow(menu, position);
            }
        }

        public static void MenuShowContext(DebugUIMenu menu)
        {
            MenuShowContext(menu, new Vector2(150F, 300F));
        }

        public static void MenuShowContext(DebugUIMenu menu, Vector2 size)
        {
            Vector2 mouse = Event.current.mousePosition;

            using (new DebugUIMatrixScope(Matrix4x4.identity))
            {
                Vector2 screenPoint = GUIUtility.GUIToScreenPoint(mouse);
                var position = new Rect(screenPoint, size);

                MenuShow(menu, position);
            }
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
