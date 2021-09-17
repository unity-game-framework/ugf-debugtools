using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public readonly struct DebugUIColorScope : IDisposable
    {
        private readonly Color m_color;

        public DebugUIColorScope(Color color)
        {
            m_color = GUI.color;

            GUI.color = color;
        }

        public void Dispose()
        {
            GUI.color = m_color;
        }
    }
}
