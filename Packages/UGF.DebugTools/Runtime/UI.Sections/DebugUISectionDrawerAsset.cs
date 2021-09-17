﻿using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Section Drawer", order = 2000)]
    public class DebugUISectionDrawerAsset : DebugUIDrawerAsset
    {
        [SerializeField] private List<AssetReference<DebugUISectionAsset>> m_sections = new List<AssetReference<DebugUISectionAsset>>();

        public List<AssetReference<DebugUISectionAsset>> Sections { get { return m_sections; } }

        protected override IDebugUIDrawer OnBuild()
        {
            var drawer = new DebugUISectionDrawer();

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