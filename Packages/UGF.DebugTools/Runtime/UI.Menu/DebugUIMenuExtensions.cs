using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public static class DebugUIMenuExtensions
    {
        public static void Add(this DebugUIMenu menu, GUIContent content, bool enabled = false)
        {
            Add(menu, content, enabled, item => { });
        }

        public static void Add(this DebugUIMenu menu, GUIContent content, bool enabled, DebugUIMenuItemHandler handler)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            var item = new DebugUIMenuItemContent(content, enabled, handler);

            menu.Add(item);
        }

        public static void ShowDropdown(this DebugUIMenu menu, Rect dropdownPosition)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            DebugUI.MenuShowDropdown(menu, dropdownPosition);
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
