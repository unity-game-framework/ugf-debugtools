using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Function Drawer", order = 2000)]
    public class DebugUIFunctionDrawerAsset : DebugUIDrawerAsset
    {
        [SerializeField] private bool m_display = true;
        [SerializeField] private bool m_displayMenuDisableEventSystem = true;
        [SerializeField] private float m_width = 200F;
        [SerializeField] private List<DebugUIFunctionAsset> m_functions = new List<DebugUIFunctionAsset>();

        public bool Display { get { return m_display; } set { m_display = value; } }
        public bool DisplayMenuDisableEventSystem { get { return m_displayMenuDisableEventSystem; } set { m_displayMenuDisableEventSystem = value; } }
        public float Width { get { return m_width; } set { m_width = value; } }
        public List<DebugUIFunctionAsset> Functions { get { return m_functions; } }

        protected override DebugUIDrawer OnBuild()
        {
            var drawer = new DebugUIFunctionDrawer
            {
                Display = m_display,
                DisplayMenuDisableEventSystem = m_displayMenuDisableEventSystem,
                Width = m_width
            };

            for (int i = 0; i < m_functions.Count; i++)
            {
                DebugUIFunctionAsset asset = m_functions[i];

                drawer.Add(asset.GroupName, asset.Build());
            }

            return drawer;
        }
    }
}
