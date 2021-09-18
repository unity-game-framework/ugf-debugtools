﻿using System;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenuDrawer : DebugUIWindowDrawer
    {
        public Rect Position { get; set; }
        public DebugUIMenu Menu { get { return m_menu ?? throw new ArgumentException("Value not specified."); } }
        public bool HasMenu { get { return m_menu != null; } }

        private readonly GUILayoutOption[] m_scrollOptions = { GUILayout.ExpandHeight(false) };
        private Vector2 m_scroll;
        private DebugUIMenu m_menu;

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
                base.OnDrawGUI();
            }
        }

        protected override Rect OnGetPosition()
        {
            return Position;
        }

        protected override void OnDrawGUILayout()
        {
            bool anySelected = false;

            using (new DebugUILayoutAreaScope(Position))
            using (new DebugUIVerticalScope(GUIContent.none, GUI.skin.box))
                // using (var view = new DebugUIScrollViewScope(m_scroll, false, false, m_scrollOptions))
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

                if (m_menu.Items.Count == 0)
                {
                    GUILayout.Label("Empty");
                }

                // m_scroll = view.ScrollPosition;
            }

            if (Event.current.type == EventType.MouseUp)
            {
                Rect position = GUILayoutUtility.GetLastRect();

                if (!position.Contains(Event.current.mousePosition))
                {
                    anySelected = true;
                }
            }

            if (anySelected)
            {
                ClearMenu();
            }
        }
    }
}
