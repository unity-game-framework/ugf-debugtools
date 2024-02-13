using System;
using UGF.DebugTools.Runtime.UI.Functions;
using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Scopes;
using UGF.EditorTools.Runtime.Ids;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public class DebugUISectionDrawer : DebugUIWindowDrawer
    {
        public Provider<GlobalId, DebugUISection> Sections { get; } = new Provider<GlobalId, DebugUISection>();
        public Vector2 Size { get; set; } = new Vector2(200F, 200F);
        public DebugUISectionAlignment Alignment { get; set; } = DebugUISectionAlignment.Left;
        public GlobalId Selected { get { return m_selected ?? throw new ArgumentException("Value not specified."); } }
        public bool HasSelected { get { return m_selected.HasValue; } }

        private readonly Func<DebugUIMenu> m_onMenuCreateFunction;
        private readonly DebugUIMenuItemHandler m_onMenuItemHandler;
        private DebugUIFunction m_functionDisplay;
        private GlobalId? m_selected;

        public DebugUISectionDrawer()
        {
            m_onMenuCreateFunction = OnMenuSectionsCreate;
            m_onMenuItemHandler = OnMenuSectionsSelected;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((_, DebugUISection section) in Sections)
            {
                section.Initialize();
            }

            m_functionDisplay = DebugUI.AddFunction(DebugUI.DebugFunctionGroupName, "Sections Display", OnFunctionDisplay);
            m_functionDisplay.Enabled = Display;
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach ((_, DebugUISection section) in Sections)
            {
                section.Uninitialize();
            }

            DebugUI.RemoveFunction(DebugUI.DebugFunctionGroupName, m_functionDisplay);
        }

        public void Clear()
        {
            ClearSelected();

            foreach ((_, DebugUISection section) in Sections)
            {
                section.Uninitialize();
            }

            Sections.Clear();
        }

        public bool SetSelected(GlobalId id)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

            ClearSelected();

            if (Sections.TryGet(id, out DebugUISection section))
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
                if (Sections.TryGet(Selected, out DebugUISection section))
                {
                    section.Deselect();
                }

                m_selected = default;
            }
        }

        protected override void OnUpdatePosition()
        {
            base.OnUpdatePosition();

            Rect screen = DebugUIUtility.GetScreenRect();

            Position = OnGetPosition(screen, Alignment, Size);
        }

        protected override void OnDrawGUILayout()
        {
            DebugUI.MenuDropdown(OnGetSelectedDisplayName(), m_onMenuCreateFunction);

            if (HasSelected)
            {
                if (Sections.TryGet(Selected, out DebugUISection section))
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
                return Sections.TryGet(Selected, out DebugUISection section)
                    ? section.DisplayName
                    : "Unknown";
            }

            return "None";
        }

        private DebugUIMenu OnMenuSectionsCreate()
        {
            var menu = new DebugUIMenu();

            menu.Add("None", !HasSelected, m_onMenuItemHandler);

            foreach ((GlobalId id, DebugUISection section) in Sections)
            {
                menu.Add(section.DisplayName, HasSelected && Selected == id, m_onMenuItemHandler, id);
            }

            return menu;
        }

        private void OnMenuSectionsSelected(DebugUIMenuItem item)
        {
            if (item.HasValue)
            {
                var id = item.GetValue<GlobalId>();

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

        private Rect OnGetPosition(Rect screen, DebugUISectionAlignment alignment, Vector2 size)
        {
            return alignment switch
            {
                DebugUISectionAlignment.Top => new Rect(0F, 0F, screen.width, size.y),
                DebugUISectionAlignment.Bottom => new Rect(0F, screen.height - size.y, screen.width, size.y),
                DebugUISectionAlignment.Right => new Rect(screen.width - size.x, 0F, size.x, screen.height),
                DebugUISectionAlignment.Left => new Rect(0F, 0F, size.x, screen.height),
                DebugUISectionAlignment.Full => new Rect(0F, 0F, screen.width, screen.height),
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, "Debug UI section alignment is unknown.")
            };
        }
    }
}
