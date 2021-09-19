using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Function Drawer", order = 2000)]
    public class DebugUIFunctionDrawerAsset : DebugUIDrawerAsset
    {
        [SerializeField] private bool m_display = true;
        [SerializeField] private float m_width = 200F;

        public bool Display { get { return m_display; } set { m_display = value; } }
        public float Width { get { return m_width; } set { m_width = value; } }

        protected override IDebugUIDrawer OnBuild()
        {
            return new DebugUIFunctionDrawer
            {
                Display = m_display,
                Width = m_width
            };
        }
    }
}
