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
        public string Selected { get { return HasSelected ? m_selected : throw new ArgumentException("Value not specified."); } }
        public bool HasSelected { get { return !string.IsNullOrEmpty(m_selected); } }

        private readonly Dictionary<string, DebugUISection> m_sections = new Dictionary<string, DebugUISection>();
        private string m_selected;
        private Vector2 m_scroll;

        public DebugUISectionDrawer()
        {
            Sections = new ReadOnlyDictionary<string, DebugUISection>(m_sections);
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
            using (new DebugUILayoutAreaScope(DebugUIUtility.GetScreenRect()))
            {
                Display = GUILayout.Toggle(Display, "Display Debug Sections");

                if (Display)
                {
                    DebugUI.MenuDropdown(OnGetSelectedDisplayName(), OnMenuSectionsCreate);

                    using (new DebugUIVerticalScope(GUIContent.none, GUI.skin.window))
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
