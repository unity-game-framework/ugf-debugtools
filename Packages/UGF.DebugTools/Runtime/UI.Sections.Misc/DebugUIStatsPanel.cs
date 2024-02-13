using System;
using UGF.DebugTools.Runtime.UI.Panels;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    public class DebugUIStatsPanel : DebugUIPanel
    {
        public DebugUIStatsAlignment Alignment { get; set; }
        public bool DisplayFps { get; set; }

        private float m_timer;
        private string m_fps;

        protected override void OnUpdatePosition()
        {
            base.OnUpdatePosition();

            Rect screen = DebugUIUtility.GetScreenRect();

            Position = OnGetPosition(screen, Alignment, Position.size);
        }

        protected override void OnDrawGUILayout()
        {
            if (DisplayFps)
            {
                GUILayout.Label(m_fps);
            }

            m_timer += Time.unscaledDeltaTime;

            if (m_timer > 1F)
            {
                if (DisplayFps)
                {
                    m_fps = (1F / Time.unscaledDeltaTime).ToString("F1");
                }

                m_timer = 0F;
            }
        }

        private Rect OnGetPosition(Rect screen, DebugUIStatsAlignment alignment, Vector2 size)
        {
            return alignment switch
            {
                DebugUIStatsAlignment.TopLeft => new Rect(0F, 0F, size.x, size.y),
                DebugUIStatsAlignment.TopRight => new Rect(screen.width - size.x, 0F, size.x, size.y),
                DebugUIStatsAlignment.BottomRight => new Rect(screen.width - size.x, screen.height - size.y, size.x, size.y),
                DebugUIStatsAlignment.BottomLeft => new Rect(0F, screen.height - size.y, size.x, size.y),
                _ => throw new ArgumentOutOfRangeException(nameof(alignment), alignment, "Debug UI stats alignment is unknown.")
            };
        }
    }
}
