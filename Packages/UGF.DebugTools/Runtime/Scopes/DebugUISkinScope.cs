using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Scopes
{
    public readonly struct DebugUISkinScope : IDisposable
    {
        private readonly GUISkin m_skin;

        public DebugUISkinScope(GUISkin skin)
        {
            m_skin = GUI.skin;

            GUI.skin = skin;
        }

        public void Dispose()
        {
            GUI.skin = m_skin;
        }
    }
}
