using System;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public readonly struct DebugUIIndentIncrementScope : IDisposable
    {
        private readonly int m_indent;

        public DebugUIIndentIncrementScope(int value)
        {
            m_indent = DebugUI.IndentLevel;

            DebugUI.IndentLevel += value;
        }

        public void Dispose()
        {
            DebugUI.IndentLevel = m_indent;
        }
    }
}
