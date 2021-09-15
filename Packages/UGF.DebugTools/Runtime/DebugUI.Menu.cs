using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        private static readonly DebugUIMenuDrawer m_menu = new DebugUIMenuDrawer();

        public static void MenuShow(DebugUIMenu menu, Rect position)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            m_menu.Position = position;
            m_menu.SetMenu(menu);
        }

        private static void MenuCheck()
        {
            if (m_menu.AnySelected())
            {
                m_menu.ClearMenu();
            }
        }
    }
}
