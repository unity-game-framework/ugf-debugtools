using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugUIDrawerComponent : MonoBehaviour
    {
        private TestPanel m_panel;

        private class TestPanel : DebugUIPanelText
        {
            public TestPanel()
            {
                Scale = Vector2.one * 2F;
            }

            protected override void OnDrawGUILayout()
            {
                base.OnDrawGUILayout();

                GUILayout.Button("Button");
            }
        }

        private void Start()
        {
            m_panel = DebugUI.PanelAdd<TestPanel>();
        }

        private void Update()
        {
            m_panel.Position = transform.position;
        }
    }
}
