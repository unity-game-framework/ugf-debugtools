using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuDrawer : DebugUIDrawerBase
    {
        public Rect Position { get; set; }
        public DebugUIMenu Menu { get { return m_menu ?? throw new ArgumentException("Value not specified."); } }
        public bool HasMenu { get { return m_menu != null; } }

        private readonly GUI.WindowFunction m_windowFunction;
        private DebugUIMenu m_menu;

        public DebugUIMenuDrawer()
        {
            m_windowFunction = OnWindow;
        }

        public void SetMenu(DebugUIMenu menu)
        {
            m_menu = menu ?? throw new ArgumentNullException(nameof(menu));
        }

        public void ClearMenu()
        {
            m_menu = null;
        }

        public bool AnySelected()
        {
            DebugUIMenu menu = Menu;

            for (int i = 0; i < menu.Items.Count; i++)
            {
                DebugUIMenuItem item = menu.Items[i];

                if (item.Selected)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void OnDrawGUI()
        {
            if (HasMenu)
            {
                GUI.ModalWindow(0, Position, m_windowFunction, GUIContent.none, GUI.skin.box);
                GUI.FocusWindow(0);
            }
        }

        private void OnWindow(int id)
        {
            for (int i = 0; i < m_menu.Items.Count; i++)
            {
                DebugUIMenuItem item = m_menu.Items[i];

                item.DrawGUILayout();
            }
        }
    }
}
