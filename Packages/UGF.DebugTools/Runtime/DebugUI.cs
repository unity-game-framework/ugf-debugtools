using System;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static DebugUIProvider Provider { get { return m_provider ?? throw new ArgumentException("Value not specified."); } }
        public static bool HasProvider { get { return m_provider != null; } }

        private static DebugUIProvider m_provider;

        public static void SetProvider(DebugUIProvider provider)
        {
            m_provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public static void ClearProvider()
        {
            m_provider = null;
        }
    }
}
