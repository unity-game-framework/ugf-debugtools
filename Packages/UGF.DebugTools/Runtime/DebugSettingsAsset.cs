using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugSettingsAsset : CustomSettingsData
    {
        [SerializeField] private Vector2 m_uiScale = Vector2.one;
        [SerializeField] private GUISkin m_uiSkin;

        public Vector2 UIScale { get { return m_uiScale; } set { m_uiScale = value; } }
        public GUISkin UISkin { get { return m_uiSkin; } set { m_uiSkin = value; } }
    }
}
