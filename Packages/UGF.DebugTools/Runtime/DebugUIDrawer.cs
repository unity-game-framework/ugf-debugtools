using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugUIDrawer
    {
        public IReadOnlyCollection<DebugUIPanel> Panels { get; }
        public Vector2 Scale { get; set; } = Vector2.one;
        public GUISkin Skin { get { return HasSkin ? m_skin : throw new ArgumentException("Value not specified."); } }
        public bool HasSkin { get { return m_skin != null; } }

        private readonly List<DebugUIPanel> m_panels = new List<DebugUIPanel>();
        private GUISkin m_skin;

        public DebugUIDrawer()
        {
            Panels = new ReadOnlyCollection<DebugUIPanel>(m_panels);
        }

        public void DrawGUI()
        {
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Scale.x, Scale.y, 1F));
            GUISkin skin = HasSkin ? Skin : GUI.skin;

            using (new DebugUIMatrixScope(matrix))
            using (new DebugUISkinScope(skin))
            {
                foreach (DebugUIPanel panel in m_panels)
                {
                    panel.DrawGUI();
                }
            }
        }

        public void AddPanel(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            panel.Scale = Scale;

            m_panels.Add(panel);
        }

        public bool RemovePanel(DebugUIPanel panel)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));

            if (m_panels.Remove(panel))
            {
                panel.Scale = Vector2.one;
                return true;
            }

            return false;
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
