using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Scopes
{
    public readonly struct DebugUIEnabledScope : IDisposable
    {
        private readonly bool m_enabled;

        public DebugUIEnabledScope(bool enabled)
        {
            m_enabled = GUI.enabled;

            GUI.enabled = enabled;
        }

        public void Dispose()
        {
            GUI.enabled = m_enabled;
        }
    }
}
