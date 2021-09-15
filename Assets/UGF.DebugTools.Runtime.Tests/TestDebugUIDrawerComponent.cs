using UGF.DebugTools.Runtime.Scopes;
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
            }
        }

        private void Start()
        {
            m_panel = DebugUI.PanelAdd<TestPanel>();
            m_panel.BindTransform(transform);
        }
    }
}
