using System;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuDrawer : DebugUIWindowDrawer
    {
        public DebugUIMenu Menu { get { return m_menu ?? throw new ArgumentException("Value not specified."); } }
        public bool HasMenu { get { return m_menu != null; } }

        private readonly GUILayoutOption[] m_scrollOptions = { GUILayout.ExpandHeight(false) };
        private DebugUIMenu m_menu;
        private Vector2 m_scroll;
        private Rect m_rect;

        public DebugUIMenuDrawer()
        {
            DisplayBackground = false;
            IsModal = true;
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
                Rect position = Position;

                Position = DebugUIUtility.GetScreenRect();

                base.OnDrawGUI();

                Position = position;
            }
        }

        protected override void OnDrawGUILayout()
        {
            bool selected = false;

            using (new DebugUILayoutAreaScope(Position))
            {
                using (new DebugUIVerticalScope(GUIContent.none, GUI.skin.window))
                using (var view = new DebugUIScrollViewScope(m_scroll, false, false, m_scrollOptions))
                {
                    for (int i = 0; i < m_menu.Items.Count; i++)
                    {
                        DebugUIMenuItem item = m_menu.Items[i];

                        item.DrawGUILayout();

                        if (item.Selected)
                        {
                            selected = true;
                        }
                    }

                    if (m_menu.Items.Count == 0)
                    {
                        GUILayout.Label("Empty");
                    }

                    m_scroll = view.ScrollPosition;
                }

                if (Event.current.type == EventType.Repaint)
                {
                    m_rect = GUILayoutUtility.GetLastRect();
                    m_rect.position = Position.position;
                }
            }

            if (Event.current.type == EventType.MouseUp)
            {
                if (!m_rect.Contains(Event.current.mousePosition))
                {
                    selected = true;
                }
            }

            if (selected)
            {
                ClearMenu();
            }
        }
    }
}
