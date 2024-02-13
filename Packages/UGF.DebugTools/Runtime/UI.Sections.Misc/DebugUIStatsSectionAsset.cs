using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Stats Section", order = 2000)]
    public class DebugUIStatsSectionAsset : DebugUISectionDescribedAsset<DebugUIStatsSection, DebugUIStatsSectionDescription>
    {
        [SerializeField] private DebugUIStatsAlignment m_alignment;
        [SerializeField] private bool m_display;
        [SerializeField] private bool m_displayBackground;
        [SerializeField] private bool m_displayEnabled;
        [SerializeField] private bool m_displayFps;

        public DebugUIStatsAlignment Alignment { get { return m_alignment; } set { m_alignment = value; } }
        public bool Display { get { return m_display; } set { m_display = value; } }
        public bool DisplayBackground { get { return m_displayBackground; } set { m_displayBackground = value; } }
        public bool DisplayEnabled { get { return m_displayEnabled; } set { m_displayEnabled = value; } }
        public bool DisplayFps { get { return m_displayFps; } set { m_displayFps = value; } }

        protected override DebugUIStatsSectionDescription OnBuildDescription()
        {
            return new DebugUIStatsSectionDescription(
                m_alignment,
                m_display,
                m_displayBackground,
                m_displayEnabled,
                m_displayFps
            );
        }

        protected override DebugUIStatsSection OnBuild(DebugUIStatsSectionDescription description)
        {
            return new DebugUIStatsSection(description);
        }
    }
}
