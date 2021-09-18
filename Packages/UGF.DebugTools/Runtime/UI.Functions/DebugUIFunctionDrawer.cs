using System;
using System.Collections.Generic;
using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public class DebugUIFunctionDrawer : DebugUIWindowDrawer
    {
        public bool DisplayMenu { get; set; }

        private readonly Dictionary<string, List<DebugUIFunction>> m_functions = new Dictionary<string, List<DebugUIFunction>>();
        private readonly Dictionary<string, List<DebugUIFunction>> m_functionsUpdate = new Dictionary<string, List<DebugUIFunction>>();
        private readonly GUILayoutOption[] m_buttonDebugOptions = { GUILayout.Width(DebugUI.LineHeight) };
        private readonly GUILayoutOption[] m_buttonMenuOptions = { GUILayout.ExpandWidth(false) };

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

            Sort(functions);
        }

        public bool Remove(string groupName, DebugUIFunction function)
        {
            if (string.IsNullOrEmpty(groupName)) throw new ArgumentException("Value cannot be null or empty.", nameof(groupName));
            if (function == null) throw new ArgumentNullException(nameof(function));

            if (m_functions.TryGetValue(groupName, out List<DebugUIFunction> functions) && functions.Remove(function))
            {
                if (functions.Count > 0)
                {
                    Sort(functions);
                }
                else
                {
                    m_functions.Remove(groupName);
                }

                return true;
            }

            return false;
        }

        protected override void OnUpdatePosition()
        {
            base.OnUpdatePosition();

            Rect screen = DebugUIUtility.GetScreenRect();

            screen.height = DebugUI.LineHeight;

            Position = screen;
        }

        protected override void OnDrawGUILayout()
        {
            if (DisplayMenu && Event.current.type == EventType.Repaint)
            {
                GUI.skin.window.Draw(Position, false, false, false, false);
            }

            using (new DebugUIHorizontalScope(GUIContent.none))
            {
                if (GUILayout.Button(GUIContent.none, m_buttonDebugOptions))
                {
                    DisplayMenu = !DisplayMenu;
                }

                if (DisplayMenu)
                {
                    foreach (KeyValuePair<string, List<DebugUIFunction>> pair in m_functions)
                    {
                        m_functionsUpdate.Add(pair.Key, pair.Value);
                    }

                    foreach (KeyValuePair<string, List<DebugUIFunction>> pair in m_functionsUpdate)
                    {
                        if (GUILayout.Button(pair.Key, DebugUIStyles.FrameHighlight, m_buttonMenuOptions))
                        {
                            DebugUIMenu menu = OnCreateMenu(pair.Value);

                            DebugUI.MenuShowContext(menu);
                        }
                    }

                    m_functionsUpdate.Clear();
                }
            }
        }

        private DebugUIMenu OnCreateMenu(List<DebugUIFunction> functions)
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

        private void Sort(List<DebugUIFunction> functions)
        {
            functions.Sort((x, y) => string.Compare(x.Content.text, y.Content.text, StringComparison.Ordinal));
        }
    }
}
