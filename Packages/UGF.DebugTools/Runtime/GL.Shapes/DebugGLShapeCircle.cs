using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Shapes
{
    public class DebugGLShapeCircle : DebugGLShape
    {
        public DebugGLShapeCircle(int segments, float degrees, DebugGLMode mode, Material material = null) : base(mode, material)
        {
            DebugGLUtility.AddVerticesCircle(Vertices, segments, degrees);
        }
    }
}
