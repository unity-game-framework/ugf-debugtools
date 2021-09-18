using System;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public readonly struct DebugUIIndentLevelScope : IDisposable
    {
        private readonly int m_level;

        public DebugUIIndentLevelScope(int level)
        {
            m_level = DebugUI.IndentLevel;

            DebugUI.IndentLevel = level;
        }

        public void Dispose()
        {
            DebugUI.IndentLevel = m_level;
        }
    }
}
