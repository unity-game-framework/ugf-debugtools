using System;
using System.Collections.Generic;
using UGF.DebugTools.Runtime.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugUIDrawer
    {
        public HashSet<DebugUIPanel> Panels { get; } = new HashSet<DebugUIPanel>();
        public GUISkin Skin { get { return HasSkin ? m_skin : throw new ArgumentException("Value not specified."); } }
        public bool HasSkin { get { return m_skin != null; } }

        private GUISkin m_skin;

        public void DrawGUI()
        {
            GUISkin skin = HasSkin ? Skin : GUI.skin;

            using (new DebugUISkinScope(skin))
            {
                foreach (DebugUIPanel panel in Panels)
                {
                    panel.DrawGUI();
                }
            }
        }

        public void SetSkin(GUISkin skin)
        {
            if (skin == null) throw new ArgumentNullException(nameof(skin));

            m_skin = skin;
        }

        public void ClearSkin()
        {
            m_skin = null;
        }
    }
}
