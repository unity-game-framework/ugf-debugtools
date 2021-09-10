﻿using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugUIDrawerComponent : MonoBehaviour
    {
        private readonly DebugUIDrawer m_drawer = new DebugUIDrawer();
        private readonly TestPanel m_panel = new TestPanel();

        private class TestPanel : DebugUIPanel
        {
            protected override void OnDrawGUILayout()
            {
                GUILayout.Button("Button");
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
