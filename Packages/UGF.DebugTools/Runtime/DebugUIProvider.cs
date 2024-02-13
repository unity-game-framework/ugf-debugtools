using System;
using UGF.DebugTools.Runtime.UI.Scopes;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugUIProvider : InitializeBase
    {
        public Provider<GlobalId, DebugUIDrawer> Drawers { get; } = new Provider<GlobalId, DebugUIDrawer>();
        public bool Enable { get; set; } = true;
        public Vector2 Scale { get; set; } = Vector2.one;
        public GUISkin Skin { get { return HasSkin ? m_skin : throw new ArgumentException("Value not specified."); } }
        public bool HasSkin { get { return m_skin != null; } }

        private readonly Provider<GlobalId, DebugUIDrawer> m_update = new Provider<GlobalId, DebugUIDrawer>();
        private GUISkin m_skin;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((_, DebugUIDrawer drawer) in Drawers)
            {
                drawer.Initialize();
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            foreach ((_, DebugUIDrawer drawer) in Drawers)
            {
                drawer.Uninitialize();
            }
        }

        public void Clear()
        {
            foreach ((_, DebugUIDrawer drawer) in Drawers)
            {
                drawer.Uninitialize();
            }

            Drawers.Clear();
        }

        public void DrawGUI()
        {
            if (Enable)
            {
                foreach ((GlobalId id, DebugUIDrawer drawer) in Drawers)
                {
                    m_update.Add(id, drawer);
                }

                Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Scale.x, Scale.y, 1F));
                GUISkin skin = HasSkin ? Skin : GUI.skin;

                using (new DebugUIMatrixScope(matrix))
                using (new DebugUISkinScope(skin))
                {
                    foreach ((_, DebugUIDrawer drawer) in m_update)
                    {
                        drawer.DrawGUI();
                    }
                }

                m_update.Clear();
            }
        }

        public void SetSkin(GUISkin skin)
        {
            m_skin = skin ? skin : throw new ArgumentNullException(nameof(skin));
        }

        public void ClearSkin()
        {
            m_skin = null;
        }
    }
}
