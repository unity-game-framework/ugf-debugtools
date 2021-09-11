using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Scopes
{
    public readonly struct DebugUIBackgroundColorScope : IDisposable
    {
        private readonly Color m_color;

        public DebugUIBackgroundColorScope(Color color)
        {
            m_color = GUI.backgroundColor;

            GUI.backgroundColor = color;
        }

        public void Dispose()
        {
            GUI.backgroundColor = m_color;
        }
    }
}
