using System.Collections.Generic;
using UGF.EditorTools.Runtime.Assets;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Menu Element", order = 2000)]
    public class DebugUIMenuElementAsset : DebugUIElementAsset
    {
        [SerializeField] private List<AssetIdReference<DebugUIElementAsset>> m_elements = new List<AssetIdReference<DebugUIElementAsset>>();

        public List<AssetIdReference<DebugUIElementAsset>> Elements { get { return m_elements; } }

        protected override DebugUIElement OnBuild()
        {
            var menus = new Dictionary<GlobalId, DebugUIElement>();

            for (int i = 0; i < m_elements.Count; i++)
            {
                AssetIdReference<DebugUIElementAsset> reference = m_elements[i];

                DebugUIElement element = reference.Asset.Build();

                element.name = reference.Asset.name;

                menus.Add(reference.Guid, element);
            }

            return new DebugUIMenuElement(menus);
        }
    }
}
