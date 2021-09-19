using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugUISettingsAsset : CustomSettingsData
    {
        [SerializeField] private bool m_enable = true;
        [SerializeField] private float m_scale = 1F;
        [SerializeField] private GUISkin m_skin;
        [SerializeField] private List<AssetReference<DebugUIDrawerAsset>> m_drawers = new List<AssetReference<DebugUIDrawerAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public float Scale { get { return m_scale; } set { m_scale = value; } }
        public GUISkin Skin { get { return m_skin; } set { m_skin = value; } }
        public List<AssetReference<DebugUIDrawerAsset>> Drawers { get { return m_drawers; } }
    }
}
