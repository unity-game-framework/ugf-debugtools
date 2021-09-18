using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public class DebugUISectionDrawer : DebugUIDrawerBase
    {
        public IReadOnlyDictionary<string, DebugUISection> Sections { get; }
        public bool Display { get; set; }
        public Vector4 PaddingRatio { get; set; } = Vector4.zero;
        public string Selected { get { return HasSelected ? m_selected : throw new ArgumentException("Value not specified."); } }
        public bool HasSelected { get { return !string.IsNullOrEmpty(m_selected); } }

        private readonly Dictionary<string, DebugUISection> m_sections = new Dictionary<string, DebugUISection>();
        private readonly GUI.WindowFunction m_windowFunction;
        private readonly Func<DebugUIMenu> m_onMenuCreateFunction;
        private string m_selected;
        private Vector2 m_scroll;
        private int? m_windowId;

        public DebugUISectionDrawer()
        {
            Sections = new ReadOnlyDictionary<string, DebugUISection>(m_sections);

            m_windowFunction = OnWindow;
            m_onMenuCreateFunction = OnMenuSectionsCreate;
        }

        public void Add(string id, DebugUISection section)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (section == null) throw new ArgumentNullException(nameof(section));

            m_sections.Add(id, section);

            section.Enable();
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (m_sections.TryGetValue(id, out DebugUISection section))
            {
                section.Disable();
                m_sections.Remove(id);
                return true;
            }

            return false;
        }

        public void Clear()
        {
            ClearSelected();

            foreach (KeyValuePair<string, DebugUISection> pair in m_sections)
            {
                pair.Value.Disable();
            }

            m_sections.Clear();
        }

        public bool SetSelected(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            ClearSelected();

            if (m_sections.TryGetValue(id, out DebugUISection section))
            {
                m_selected = id;
                section.Select();

                return false;
            }

            return true;
        }

        public void ClearSelected()
        {
            if (HasSelected)
            {
                if (m_sections.TryGetValue(m_selected, out DebugUISection section))
                {
                    section.Deselect();
                }

                m_selected = string.Empty;
            }
        }

        protected override void OnDrawGUI()
        {
            Rect screen = DebugUIUtility.GetScreenRect();

            screen.min += new Vector2(screen.width * PaddingRatio.x, screen.height * PaddingRatio.y);
            screen.max -= new Vector2(screen.width * PaddingRatio.z, screen.height * PaddingRatio.w);

            if (Display)
            {
                m_windowId ??= GetHashCode();

                GUI.Window(m_windowId.Value, screen, m_windowFunction, GUIContent.none);
            }
        }

        private void OnWindow(int id)
        {
            if (Display)
            {
                DebugUI.MenuDropdown(OnGetSelectedDisplayName(), m_onMenuCreateFunction);

                using (var view = new DebugUIScrollViewScope(m_scroll))
                {
                    if (HasSelected)
                    {
                        if (m_sections.TryGetValue(Selected, out DebugUISection section))
                        {
                            section.DrawGUILayout();
                        }
                        else
                        {
                            GUILayout.Label("Section not found.");
                        }
                    }
                    else
                    {
                        GUILayout.Label("Section not selected.");
                    }

                    m_scroll = view.ScrollPosition;
                }
            }
        }

        private GUIContent OnGetSelectedDisplayName()
        {
            if (HasSelected)
            {
                return m_sections.TryGetValue(Selected, out DebugUISection section)
                    ? new GUIContent(section.DisplayName)
                    : new GUIContent("Unknown");
            }

            return new GUIContent("None");
        }

        private DebugUIMenu OnMenuSectionsCreate()
        {
            var menu = new DebugUIMenu();

            foreach (KeyValuePair<string, DebugUISection> pair in m_sections)
            {
                menu.Add(new GUIContent(pair.Value.DisplayName), m_selected == pair.Key, OnMenuSectionsSelected, pair.Key);
            }

            return menu;
        }

        private void OnMenuSectionsSelected(DebugUIMenuItem item)
        {
            string id = item.GetValue<string>();

            SetSelected(id);
        }
    }
}
