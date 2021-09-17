using System;
using UGF.DebugTools.Runtime.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuDrawer : DebugUIDrawerBase
    {
        public Rect Position { get; set; }
        public DebugUIMenu Menu { get { return m_menu ?? throw new ArgumentException("Value not specified."); } }
        public bool HasMenu { get { return m_menu != null; } }

        private readonly GUI.WindowFunction m_windowFunction;
        private readonly GUILayoutOption[] m_scrollOptions = { GUILayout.ExpandHeight(false) };
        private Vector2 m_scroll;
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

        protected override void OnDrawGUI()
        {
            if (HasMenu)
            {
                GUI.ModalWindow(0, Position, m_windowFunction, GUIContent.none, GUIStyle.none);
                GUI.FocusWindow(0);
            }
        }

        private void OnWindow(int id)
        {
            bool anySelected = false;

            using (new DebugUIVerticalScope(GUIContent.none, GUI.skin.box))
            using (var view = new DebugUIScrollViewScope(m_scroll, false, false, m_scrollOptions))
            {
                for (int i = 0; i < m_menu.Items.Count; i++)
                {
                    DebugUIMenuItem item = m_menu.Items[i];

                    item.DrawGUILayout();

                    if (item.Selected)
                    {
                        anySelected = true;
                    }
                }

                m_scroll = view.ScrollPosition;
            }

            if (anySelected)
            {
                ClearMenu();
            }
        }
    }
}
