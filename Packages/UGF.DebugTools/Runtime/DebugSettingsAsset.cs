using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugSettingsAsset : CustomSettingsData
    {
        [SerializeField] private GUISkin m_defaultSkin;

        public GUISkin DefaultSkin { get { return m_defaultSkin; } set { m_defaultSkin = value; } }
    }
}
