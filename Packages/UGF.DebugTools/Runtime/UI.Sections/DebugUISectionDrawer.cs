using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.UI.Functions;
using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public class DebugUISectionDrawer : DebugUIWindowDrawer
    {
        public IReadOnlyDictionary<string, DebugUISection> Sections { get; }
        public Vector2 Size { get; set; } = new Vector2(200F, 200F);
        public DebugUISectionAlignment Alignment { get; set; } = DebugUISectionAlignment.Left;
        public string Selected { get { return HasSelected ? m_selected : throw new ArgumentException("Value not specified."); } }
        public bool HasSelected { get { return !string.IsNullOrEmpty(m_selected); } }

        private readonly Dictionary<string, DebugUISection> m_sections = new Dictionary<string, DebugUISection>();
        private readonly Func<DebugUIMenu> m_onMenuCreateFunction;
        private readonly DebugUIMenuItemHandler m_onMenuItemHandler;
        private DebugUIFunction m_functionDisplay;
        private string m_selected;

        public DebugUISectionDrawer()
        {
            Sections = new ReadOnlyDictionary<string, DebugUISection>(m_sections);

            m_onMenuCreateFunction = OnMenuSectionsCreate;
            m_onMenuItemHandler = OnMenuSectionsSelected;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach (KeyValuePair<string, DebugUISection> pair in m_sections)
            {
                pair.Value.Initialize();
            }

            m_functionDisplay = DebugUI.AddFunction(DebugUI.DebugFunctionGroupName, "Sections Display", OnFunctionDisplay);
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach (KeyValuePair<string, DebugUISection> pair in m_sections)
            {
                pair.Value.Uninitialize();
            }

            DebugUI.RemoveFunction(DebugUI.DebugFunctionGroupName, m_functionDisplay);
        }

        public void Add(string id, DebugUISection section)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (section == null) throw new ArgumentNullException(nameof(section));

            m_sections.Add(id, section);
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (m_sections.TryGetValue(id, out DebugUISection section))
            {
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
                pair.Value.Uninitialize();
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

        protected override void OnUpdatePosition()
        {
            base.OnUpdatePosition();

            Rect screen = DebugUIUtility.GetScreenRect();

            Position = GetPosition(screen, Alignment, Size);
        }

        protected override void OnDrawGUILayout()
        {
            DebugUI.MenuDropdown(OnGetSelectedDisplayName(), m_onMenuCreateFunction);

            if (HasSelected)
            {
                if (m_sections.TryGetValue(Selected, out DebugUISection section))
                {
                    section.DrawGUILayout();
                }
                else
                {
                    using (new DebugUICenterScope(GUIContent.none))
                    {
                        GUILayout.Label("Section not found.");
                    }
                }
            }
            else
            {
                using (new DebugUICenterScope(GUIContent.none))
                {
                    GUILayout.Label("Section not selected.");
                }
            }
        }

        private string OnGetSelectedDisplayName()
        {
            if (HasSelected)
            {
                return m_sections.TryGetValue(Selected, out DebugUISection section)
                    ? section.DisplayName
                    : "Unknown";
            }

            return "None";
        }

        private DebugUIMenu OnMenuSectionsCreate()
        {
            var menu = new DebugUIMenu();

            menu.Add("None", !HasSelected, m_onMenuItemHandler);

            foreach (KeyValuePair<string, DebugUISection> pair in m_sections)
            {
                menu.Add(pair.Value.DisplayName, m_selected == pair.Key, m_onMenuItemHandler, pair.Key);
            }

            return menu;
        }

        private void OnMenuSectionsSelected(DebugUIMenuItem item)
        {
            if (item.HasValue)
            {
                string id = item.GetValue<string>();

                SetSelected(id);
            }
            else
            {
                ClearSelected();
            }
        }

        private void OnFunctionDisplay(DebugUIFunction function)
        {
            Display = !Display;

            function.Enabled = Display;
        }

        private Rect GetPosition(Rect screen, DebugUISectionAlignment alignment, Vector2 size)
        {
            switch (alignment)
            {
                case DebugUISectionAlignment.Top: return new Rect(0F, 0F, screen.width, size.y);
                case DebugUISectionAlignment.Bottom: return new Rect(0F, screen.height - size.y, screen.width, size.y);
                case DebugUISectionAlignment.Right: return new Rect(screen.width - size.x, 0F, size.x, screen.height);
                case DebugUISectionAlignment.Left: return new Rect(0F, 0F, size.x, screen.height);
                case DebugUISectionAlignment.Full: return new Rect(0F, 0F, screen.width, screen.height);
                default:
                    throw new ArgumentOutOfRangeException(nameof(alignment), alignment, null);
            }
        }
    }
}
