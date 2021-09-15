using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public static class DebugUIMenuExtensions
    {
        public static void Add(this DebugUIMenu menu, GUIContent content)
        {
            Add(menu, content, item => { });
        }

        public static void Add(this DebugUIMenu menu, GUIContent content, DebugUIMenuItemHandler handler)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            var item = new DebugUIMenuItemContent(content, handler);

            menu.Add(item);
        }

        public static void ShowContext(this DebugUIMenu menu)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            DebugUI.MenuShowContext(menu);
        }

        public static void Show(this DebugUIMenu menu, Rect position)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            DebugUI.MenuShow(menu, position);
        }
    }
}
