using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Panels;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugUIDrawerComponent : MonoBehaviour
    {
        private TestPanel m_panel;

        private class TestPanel : DebugUIPanelText
        {
            private Vector2 m_scroll;
            private float m_slider;

            protected override void OnDrawGUILayout()
            {
                base.OnDrawGUILayout();

                using (new DebugUIVerticalScope(GUIContent.none, GUI.skin.box))
                using (var view = new DebugUIScrollViewScope(m_scroll))
                {
                    OnDrawElements();

                    m_scroll = view.ScrollPosition;
                }
            }

            private void OnDrawElements()
            {
                if (GUILayout.Button("Menu Context"))
                {
                    var menu = new DebugUIMenu();

                    for (int i = 0; i < 10; i++)
                    {
                        menu.Add(new GUIContent($"Item {i}"));
                    }

                    menu.ShowContext();
                }

                GUILayout.Button("Button");
                GUILayout.Label("Label");
                GUILayout.TextField("TextField");
                GUILayout.Box("Box");
                GUILayout.Toolbar(1, new[] { "Tab1", "Tab2" });
                GUILayout.Toggle(true, "Toggle");
                GUILayout.PasswordField("Password", '*');
                GUILayout.TextArea("TextArea");
                GUILayout.SelectionGrid(1, new[] { "Cell1", "Cell2", "Cell3", "Cell4" }, 2);

                m_slider = GUILayout.HorizontalSlider(m_slider, 0, 1);
                m_slider = GUILayout.VerticalSlider(m_slider, 0, 1);

                m_slider = GUILayout.HorizontalScrollbar(m_slider, 0.25f, 0, 1);
                m_slider = GUILayout.VerticalScrollbar(m_slider, 0.25f, 0, 1);

                if (GUILayout.Button("Menu Context"))
                {
                    var menu = new DebugUIMenu();

                    for (int i = 0; i < 10; i++)
                    {
                        menu.Add(new GUIContent($"Item {i}"));
                    }

                    menu.ShowContext();
                }

                Rect rectDropdown = GUILayoutUtility.GetRect(new GUIContent("Menu Dropdown"), GUI.skin.button);

                if (GUI.Button(rectDropdown, "Menu Dropdown"))
                {
                    var menu = new DebugUIMenu();

                    for (int i = 0; i < 10; i++)
                    {
                        menu.Add(new GUIContent($"Item {i}"));
                    }

                    menu.ShowDropdown(rectDropdown);
                }

                GUILayout.Button("Test Last Position");
                GUI.Box(GUILayoutUtility.GetLastRect(), "Last Position");
            }
        }

        private void Start()
        {
            m_panel = DebugUI.PanelAdd<TestPanel>();
            m_panel.BindTransform(transform);
        }
    }
}
