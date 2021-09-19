using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Shapes
{
    public class DebugGLShapeQuad : DebugGLShape
    {
        public DebugGLShapeQuad(DebugGLMode mode, Material material = null) : base(mode, material)
        {
            Vertices.Add(new Vector3(-0.5F, -0.5F, 0F));
            Vertices.Add(new Vector3(-0.5F, 0.5F, 0F));
            Vertices.Add(new Vector3(0.5F, 0.5F, 0F));
            Vertices.Add(new Vector3(0.5F, -0.5F, 0F));
            Vertices.Add(new Vector3(-0.5F, -0.5F, 0F));
        }
    }
}
