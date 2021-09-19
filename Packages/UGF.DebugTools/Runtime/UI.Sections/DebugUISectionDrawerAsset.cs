using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Section Drawer", order = 2000)]
    public class DebugUISectionDrawerAsset : DebugUIDrawerAsset
    {
        [SerializeField] private float m_width = 200F;
        [SerializeField] private float m_height = 200F;
        [SerializeField] private DebugUISectionAlignment m_alignment = DebugUISectionAlignment.Right;
        [SerializeField] private List<AssetReference<DebugUISectionAsset>> m_sections = new List<AssetReference<DebugUISectionAsset>>();

        public float Width { get { return m_width; } set { m_width = value; } }
        public float Height { get { return m_height; } set { m_height = value; } }
        public DebugUISectionAlignment Alignment { get { return m_alignment; } set { m_alignment = value; } }
        public List<AssetReference<DebugUISectionAsset>> Sections { get { return m_sections; } }

        protected override IDebugUIDrawer OnBuild()
        {
            var drawer = new DebugUISectionDrawer
            {
                Size = new Vector2(m_width, m_height),
                Alignment = m_alignment
            };

            for (int i = 0; i < m_sections.Count; i++)
            {
                AssetReference<DebugUISectionAsset> reference = m_sections[i];
                DebugUISection section = reference.Asset.Build();

                drawer.Add(reference.Guid, section);
            }

            return drawer;
        }
    }
}
