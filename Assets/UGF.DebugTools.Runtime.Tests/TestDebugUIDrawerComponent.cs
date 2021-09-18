using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Panels;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugUIDrawerComponent : MonoBehaviour
    {
        private class TestPanel : DebugUIPanelText
        {
            private Vector2 m_scroll;
            private float m_slider;
            private int m_menuSelected;
            private string m_textArea = "Text Area";
            private int m_int = 10;
            private float m_float = 10.5F;
            private bool m_toggle;
            private bool m_foldout;

            public TestPanel()
            {
                Display = true;
            }

            protected override void OnDrawGUILayout()
            {
                base.OnDrawGUILayout();

                using (new DebugUIVerticalScope(GUIContent.none, GUI.skin.window))
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
                    OnMenuCreate().ShowContext();
                }

                DebugUI.MenuDropdown("Menu", OnMenuCreate());

                DebugUI.Header("Fields");
                DebugUI.FieldLabel("Label", "Content");
                DebugUI.FieldButton("Button", "Content");
                DebugUI.FieldButton(string.Empty, "Content");
                DebugUI.FieldDropdown("Dropdown", $"{m_menuSelected}", OnMenuCreate);
                m_toggle = DebugUI.FieldToggle("Toggle", m_toggle);
                DebugUI.FieldPassword("Password", "Password");
                DebugUI.FieldText("Text", "Text");
                m_textArea = DebugUI.FieldTextArea("Text Area", m_textArea);
                m_int = DebugUI.FieldValue("Int", m_int);
                m_float = DebugUI.FieldValue("Float", m_float);
                m_foldout = DebugUI.Foldout("Foldout", m_foldout);

                if (m_foldout)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        GUILayout.Button("Button");
                    }
                }

                DebugUI.Header("Indent");
                DebugUI.FieldButton("Button", "Content");

                using (new DebugUIIndentIncrementScope(1))
                {
                    DebugUI.FieldButton("Button", "Content");

                    using (new DebugUIIndentIncrementScope(1))
                    {
                        DebugUI.FieldButton("Button", "Content");
                    }
                }

                using (new DebugUIIndentLevelScope(3))
                {
                    DebugUI.FieldButton("Button", "Content");
                }

                DebugUI.Space();

                GUILayout.Label("Defaults");
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
                    OnMenuCreate().ShowContext();
                }

                Rect rectDropdown = GUILayoutUtility.GetRect(new GUIContent("Menu Dropdown"), GUI.skin.button);

                if (GUI.Button(rectDropdown, "Menu Dropdown"))
                {
                    OnMenuCreate().ShowDropdown(rectDropdown);
                }

                DebugUI.MenuDropdown(new GUIContent("Menu"), OnMenuCreate());

                GUILayout.Button("Test Last Position");
                GUI.Box(GUILayoutUtility.GetLastRect(), "Last Position");
            }

            private DebugUIMenu OnMenuCreate()
            {
                var menu = new DebugUIMenu();

                for (int i = 0; i < 10; i++)
                {
                    menu.Add(new GUIContent($"Item {i}"), m_menuSelected == i, OnMenuSelect, i);
                }

                return menu;
            }

            private void OnMenuSelect(DebugUIMenuItem item)
            {
                m_menuSelected = item.GetValue<int>();
            }
        }

        private void Start()
        {
            DebugUI.PanelAdd<TestPanel>().BindTransform(transform);
        }
    }
}
