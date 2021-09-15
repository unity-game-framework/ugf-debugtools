﻿using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public static class DebugUIMenuExtensions
    {
        public static void Add(this DebugUIMenu menu, GUIContent content, DebugUIMenuItemHandler handler)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            var item = new DebugUIMenuItemContent(content, handler);

            menu.Add(item);
        }

        public static void Show(this DebugUIMenu menu, Rect position)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            DebugUI.MenuShow(menu, position);
        }
    }
}
