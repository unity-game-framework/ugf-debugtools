using System;

namespace UGF.DebugTools.Runtime.GL
{
    public readonly struct DebugGLDrawScope : IDisposable
    {
        public DebugGLDrawScope(DebugGLMode mode) : this((int)mode)
        {
        }

        public DebugGLDrawScope(int mode)
        {
            UnityEngine.GL.Begin(mode);
        }

        public void Dispose()
        {
            UnityEngine.GL.End();
        }
    }
}
