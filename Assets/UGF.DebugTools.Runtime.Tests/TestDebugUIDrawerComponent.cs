using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugUIDrawerComponent : MonoBehaviour
    {
        private readonly DebugUIDrawer m_drawer = new DebugUIDrawer();
        private readonly TestPanel m_panel = new TestPanel();

        private class TestPanel : DebugUIPanel
        {
            public TestPanel()
            {
                Scale = Vector2.one * 2F;
            }

            protected override void OnDrawGUILayout()
            {
                GUILayout.BeginVertical(GUI.skin.box);

                GUILayout.Button("Button");

                GUILayout.EndVertical();
            }
        }

        private void Start()
        {
            m_drawer.Panels.Add(m_panel);
        }

        private void Update()
        {
            m_panel.Position = transform.position;
        }

        private void OnGUI()
        {
            m_drawer.DrawGUI();
        }
    }
}
