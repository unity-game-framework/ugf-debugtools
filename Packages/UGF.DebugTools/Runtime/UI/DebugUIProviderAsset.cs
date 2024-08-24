using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Provider", order = 2000)]
    public class DebugUIProviderAsset : BuilderAsset<DebugUIProvider>
    {
        [SerializeField] private bool m_enable = true;
        [SerializeField] private string m_documentGameObjectName = "DebugUIDocument";
        [SerializeField] private PanelSettings m_panelSettings;
        [SerializeField] private List<AssetIdReference<DebugUIElementAsset>> m_elements = new List<AssetIdReference<DebugUIElementAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public string DocumentGameObjectName { get { return m_documentGameObjectName; } set { m_documentGameObjectName = value; } }
        public PanelSettings PanelSettings { get { return m_panelSettings; } set { m_panelSettings = value; } }
        public List<AssetIdReference<DebugUIElementAsset>> Elements { get { return m_elements; } }

        protected override DebugUIProvider OnBuild()
        {
            var provider = new DebugUIProvider(m_panelSettings, m_documentGameObjectName)
            {
                Document =
                {
                    enabled = m_enable
                }
            };

            for (int i = 0; i < m_elements.Count; i++)
            {
                AssetIdReference<DebugUIElementAsset> reference = m_elements[i];

                provider.Elements.Add(reference.Guid, reference.Asset.Build());
            }

            return provider;
        }
    }
}
