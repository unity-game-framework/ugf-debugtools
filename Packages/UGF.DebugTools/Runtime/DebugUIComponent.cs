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
        public DebugUIDrawer Drawer { get { return m_drawer ?? throw new ArgumentException("Value not specified."); } }
        public bool HasDrawer { get { return m_drawer != null; } }

        private DebugUIDrawer m_drawer;

        private void Start()
        {
            if (DebugUI.HasDrawer) throw new InvalidOperationException("Debug UI Drawer already specified.");

            m_drawer = new DebugUIDrawer
            {
                Enable = m_enable,
                Scale = Vector2.one * m_scale
            };

            if (m_skin != null)
            {
                m_drawer.SetSkin(m_skin);
            }

            for (int i = 0; i < m_drawers.Count; i++)
            {
                AssetReference<DebugUIDrawerAsset> reference = m_drawers[i];

                m_drawer.Add(reference.Guid, reference.Asset.Build());
            }

            DebugUI.DrawerSet(m_drawer);

            m_drawer.Initialize();
        }

        private void OnDestroy()
        {
            if (HasDrawer)
            {
                m_drawer.Uninitialize();

                if (DebugUI.HasDrawer && DebugUI.Drawer == m_drawer)
                {
                    DebugUI.DrawerClear();
                }

                m_drawer = null;
            }
        }

        private void OnGUI()
        {
            m_drawer.DrawGUI();

            DebugUIContentCache.Reset();
        }
    }
}
