using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public readonly struct DebugUIMatrixScope : IDisposable
    {
        private readonly Matrix4x4 m_matrix;

        public DebugUIMatrixScope(Matrix4x4 matrix)
        {
            m_matrix = GUI.matrix;

            GUI.matrix = matrix;
        }

        public void Dispose()
        {
            GUI.matrix = m_matrix;
        }
    }
}
