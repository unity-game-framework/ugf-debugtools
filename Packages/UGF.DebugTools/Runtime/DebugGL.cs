using System;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugGL
    {
        public static DebugGLProvider Provider { get { return m_provider ?? throw new ArgumentException("Value not specified."); } }
        public static bool HasProvider { get { return m_provider != null; } }

        private static DebugGLProvider m_provider;

        public static void SetProvider(DebugGLProvider provider)
        {
            m_provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public static void ClearProvider()
        {
            m_provider = null;
        }
    }
}
