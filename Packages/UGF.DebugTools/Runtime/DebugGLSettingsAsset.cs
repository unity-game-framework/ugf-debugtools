using System.Collections.Generic;
using UGF.CustomSettings.Runtime;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugGLSettingsAsset : CustomSettingsData
    {
        [SerializeField] private bool m_enable = true;
        [SerializeField] private List<AssetReference<DebugGLShapeAsset>> m_shapes = new List<AssetReference<DebugGLShapeAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public List<AssetReference<DebugGLShapeAsset>> Shapes { get { return m_shapes; } }
    }
}
