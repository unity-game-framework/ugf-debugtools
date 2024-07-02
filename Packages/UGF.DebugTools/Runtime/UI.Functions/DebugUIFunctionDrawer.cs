using System;
using System.Collections.Generic;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public class DebugUIFunctionDrawer : DebugUIWindowDrawer
    {
        public bool DisplayMenu { get; set; }
        public bool DisplayMenuDisableEventSystem { get; set; }
        public float Width { get; set; } = 200F;

        private readonly Dictionary<string, List<DebugUIFunction>> m_functions = new Dictionary<string, List<DebugUIFunction>>();
        private readonly Dictionary<string, List<DebugUIFunction>> m_functionsUpdate = new Dictionary<string, List<DebugUIFunction>>();
        private bool m_eventSystemPreviousState;

        public DebugUIFunctionDrawer()
        {
            DisplayBackground = false;
        }

        public void Add(string groupName, DebugUIFunction function)
        {
            if (string.IsNullOrEmpty(groupName)) throw new ArgumentException("Value cannot be null or empty.", nameof(groupName));
            if (function == null) throw new ArgumentNullException(nameof(function));

            if (!m_functions.TryGetValue(groupName, out List<DebugUIFunction> functions))
            {
                functions = new List<DebugUIFunction>();

                m_functions.Add(groupName, functions);
            }

            functions.Add(function);

            OnSort(functions);
        }

        public bool Remove(string groupName, DebugUIFunction function)
        {
            if (string.IsNullOrEmpty(groupName)) throw new ArgumentException("Value cannot be null or empty.", nameof(groupName));
            if (function == null) throw new ArgumentNullException(nameof(function));

            if (m_functions.TryGetValue(groupName, out List<DebugUIFunction> functions) && functions.Remove(function))
            {
                if (functions.Count > 0)
                {
                    OnSort(functions);
                }
                else
                {
                    m_functions.Remove(groupName);
                }

                return true;
            }

            return false;
        }

        public void Clear()
        {
            m_functions.Clear();
        }

        protected override void OnUpdatePosition()
        {
            base.OnUpdatePosition();

            Rect screen = DebugUIUtility.GetScreenRect();

            Position = DisplayMenu
                ? new Rect(0F, 0F, Width, screen.height)
                : new Rect(0F, 0F, 30F, 30F);
        }

        protected override void OnDrawGUILayout()
        {
            string label = DisplayMenu ? "Debug Functions" : string.Empty;

            bool change = GUILayout.Toggle(DisplayMenu, label);

            if (change != DisplayMenu)
            {
                DisplayMenu = change;
                DisplayBackground = DisplayMenu;

                if (DisplayMenuDisableEventSystem)
                {
                    OnChangeEventSystem(!change);
                }
            }

            if (DisplayMenu)
            {
                foreach ((string groupName, List<DebugUIFunction> functions) in m_functions)
                {
                    m_functionsUpdate.Add(groupName, functions);
                }

                foreach ((string groupName, List<DebugUIFunction> functions) in m_functionsUpdate)
                {
                    Rect position = DebugUI.GetControlRect();

                    if (DebugUI.Dropdown(position, groupName))
                    {
                        DebugUIMenu menu = OnCreateMenu(functions);

                        DebugUI.MenuShowDropdown(menu, position);
                    }
                }

                m_functionsUpdate.Clear();
            }
        }

        private DebugUIMenu OnCreateMenu(IReadOnlyList<DebugUIFunction> functions)
        {
            var menu = new DebugUIMenu();

            for (int i = 0; i < functions.Count; i++)
            {
                DebugUIFunction function = functions[i];

                if (function.Validate())
                {
                    menu.Add(function.Content, function.Enabled, item => item.GetValue<DebugUIFunction>().Execute(), function);
                }
                else
                {
                    menu.AddDisabled(function.Content, function.Enabled);
                }
            }

            return menu;
        }

        private void OnSort(List<DebugUIFunction> functions)
        {
            functions.Sort((x, y) => string.Compare(x.Content.text, y.Content.text, StringComparison.Ordinal));
        }

        private void OnChangeEventSystem(bool value)
        {
#if UGF_DEBUGTOOLS_UI_INSTALLED
            var eventSystem = Object.FindAnyObjectByType<EventSystem>(FindObjectsInactive.Include);

            if (eventSystem != null)
            {
                if (value)
                {
                    eventSystem.enabled = m_eventSystemPreviousState;
                }
                else
                {
                    m_eventSystemPreviousState = eventSystem.enabled;

                    eventSystem.enabled = false;
                }
            }
#endif
        }
    }
}
