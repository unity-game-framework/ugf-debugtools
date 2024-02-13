using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Provider", order = 2000)]
    public class DebugUIProviderAsset : BuilderAsset<DebugUIProvider>
    {
        [SerializeField] private bool m_enable = true;
        [SerializeField] private float m_scale = 1F;
        [SerializeField] private GUISkin m_skin;
        [SerializeField] private List<AssetIdReference<DebugUIDrawerAsset>> m_drawers = new List<AssetIdReference<DebugUIDrawerAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public float Scale { get { return m_scale; } set { m_scale = value; } }
        public GUISkin Skin { get { return m_skin; } set { m_skin = value; } }
        public List<AssetIdReference<DebugUIDrawerAsset>> Drawers { get { return m_drawers; } }

        protected override DebugUIProvider OnBuild()
        {
            var provider = new DebugUIProvider
            {
                Enable = m_enable,
                Scale = Vector2.one * m_scale
            };

            for (int i = 0; i < m_drawers.Count; i++)
            {
                AssetIdReference<DebugUIDrawerAsset> reference = m_drawers[i];

                provider.Drawers.Add(reference.Guid, reference.Asset.Build());
            }

            if (m_skin != null)
            {
                provider.SetSkin(m_skin);
            }

            return provider;
        }
    }
}
