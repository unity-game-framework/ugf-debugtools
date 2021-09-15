using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugSettingsAsset : CustomSettingsData
    {
        [SerializeField] private Vector2 m_uiScale = Vector2.one;
        [SerializeField] private GUISkin m_defaultSkin;

        public Vector2 UIScale { get { return m_uiScale; } set { m_uiScale = value; } }
        public GUISkin DefaultSkin { get { return m_defaultSkin; } set { m_defaultSkin = value; } }
    }
}
