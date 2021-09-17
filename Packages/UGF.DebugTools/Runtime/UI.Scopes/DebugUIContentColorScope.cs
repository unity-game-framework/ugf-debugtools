using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public readonly struct DebugUIContentColorScope : IDisposable
    {
        private readonly Color m_color;

        public DebugUIContentColorScope(Color color)
        {
            m_color = GUI.contentColor;

            GUI.contentColor = color;
        }

        public void Dispose()
        {
            GUI.contentColor = m_color;
        }
    }
}
