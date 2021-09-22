using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [AddComponentMenu("Unity Game Framework/Debug/Debug UI", 2000)]
    public class DebugUIComponent : MonoBehaviour
    {
        [SerializeField] private bool m_enable = true;
        [SerializeField] private float m_scale = 1F;
        [SerializeField] private GUISkin m_skin;
        [SerializeField] private List<AssetReference<DebugUIDrawerAsset>> m_drawers = new List<AssetReference<DebugUIDrawerAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public float Scale { get { return m_scale; } set { m_scale = value; } }
        public GUISkin Skin { get { return m_skin; } set { m_skin = value; } }
        public List<AssetReference<DebugUIDrawerAsset>> Drawers { get { return m_drawers; } }

        private void Start()
        {
            DebugUI.Drawer.Enable = m_enable;
            DebugUI.Drawer.Scale = Vector2.one * m_scale;

            if (m_skin != null)
            {
                DebugUI.Drawer.SetSkin(m_skin);
            }

            for (int i = 0; i < m_drawers.Count; i++)
            {
                AssetReference<DebugUIDrawerAsset> reference = m_drawers[i];

                DebugUI.Drawer.Add(reference.Guid, reference.Asset.Build());
            }
        }

        private void OnDestroy()
        {
            for (int i = m_drawers.Count - 1; i >= 0; i--)
            {
                AssetReference<DebugUIDrawerAsset> reference = m_drawers[i];

                DebugUI.Drawer.Remove(reference.Guid);
            }
        }

        private void OnGUI()
        {
            DebugUI.Drawer.DrawGUI();
            DebugUIContentCache.Reset();
        }
    }
}
