using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugGLSettingsAsset : CustomSettingsData
    {
        [SerializeField] private Material m_material;

        public Material Material { get { return m_material; } set { m_material = value; } }
    }
}
