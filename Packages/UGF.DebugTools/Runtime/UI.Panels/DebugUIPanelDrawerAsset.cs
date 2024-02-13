using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Panel Drawer", order = 2000)]
    public class DebugUIPanelDrawerAsset : DebugUIDrawerAsset
    {
        [SerializeField] private bool m_display;

        public bool Display { get { return m_display; } set { m_display = value; } }

        protected override DebugUIDrawer OnBuild()
        {
            return new DebugUIPanelDrawer
            {
                Display = m_display
            };
        }
    }
}
