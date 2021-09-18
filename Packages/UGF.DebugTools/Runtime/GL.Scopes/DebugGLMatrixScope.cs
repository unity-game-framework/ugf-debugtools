using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Scopes
{
    public struct DebugGLMatrixScope : IDisposable
    {
        public DebugGLMatrixScope(Matrix4x4 matrix)
        {
            UnityEngine.GL.PushMatrix();
            UnityEngine.GL.MultMatrix(matrix);
        }

        public void Dispose()
        {
            UnityEngine.GL.PopMatrix();
        }
    }
}
