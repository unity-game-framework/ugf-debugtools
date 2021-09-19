using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public static class DebugUIMenuExtensions
    {
        public static void AddDisabled(this DebugUIMenu menu, string content, bool enabled = false)
        {
            AddDisabled(menu, new GUIContent(content), enabled);
        }

        public static void AddDisabled(this DebugUIMenu menu, GUIContent content, bool enabled = false)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            menu.Add(new DebugUIMenuItemDisabled(content, enabled));
        }

        public static void Add(this DebugUIMenu menu, string content, bool enabled = false)
        {
            Add(menu, new GUIContent(content), enabled);
        }

        public static void Add(this DebugUIMenu menu, GUIContent content, bool enabled = false)
        {
            Add(menu, content, enabled, item => { });
        }

        public static void Add(this DebugUIMenu menu, string content, bool enabled, DebugUIMenuItemHandler handler, object value = null)
        {
            Add(menu, new GUIContent(content), enabled, handler, value);
        }

        public static void Add(this DebugUIMenu menu, GUIContent content, bool enabled, DebugUIMenuItemHandler handler, object value = null)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            menu.Add(new DebugUIMenuItemContent(content, enabled, handler, value));
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
