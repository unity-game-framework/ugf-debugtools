using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [AddComponentMenu("")]
    public class DebugUIExecutor : MonoBehaviour
    {
        public DebugUIDrawer Drawer { get { return m_drawer ?? throw new ArgumentException("Value not specified."); } }
        public bool HasDrawer { get { return m_drawer != null; } }

        private DebugUIDrawer m_drawer;

        public void SetDrawer(DebugUIDrawer drawer)
        {
            m_drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
        }

        public void ClearDrawer()
        {
            m_drawer = null;
        }

        private void OnGUI()
        {
            if (HasDrawer)
            {
                Drawer.DrawGUI();
            }
        }
    }
}
