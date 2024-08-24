using System;

namespace UGF.DebugTools.Runtime.GL
{
    public readonly struct DebugGLWireframeScope : IDisposable
    {
        private readonly bool m_wireframe;

        public DebugGLWireframeScope(bool wireframe)
        {
            m_wireframe = UnityEngine.GL.wireframe;

            UnityEngine.GL.wireframe = wireframe;
        }

        public void Dispose()
        {
            UnityEngine.GL.wireframe = m_wireframe;
        }
    }
}
