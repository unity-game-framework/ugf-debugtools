using System;
using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Misc
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Stats Element", order = 2000)]
    public class DebugUIStatsElementAsset : DebugUIElementAsset
    {
        [TimeSpanTicks]
        [SerializeField] private long m_updateInterval;

        public long UpdateInterval { get { return m_updateInterval; } set { m_updateInterval = value; } }

        protected override DebugUIElement OnBuild()
        {
            return new DebugUIStatsElement(TimeSpan.FromTicks(m_updateInterval));
        }
    }
}
