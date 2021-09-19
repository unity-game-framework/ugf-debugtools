using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Shapes
{
    public class DebugGLShapeQuadWire : DebugGLShape
    {
        public DebugGLShapeQuadWire(Material material = null) : base(material)
        {
            Mode = DebugGLMode.LineStrip;
            Vertices.Add(new Vector3(-0.5F, -0.5F, 0F));
            Vertices.Add(new Vector3(-0.5F, 0.5F, 0F));
            Vertices.Add(new Vector3(0.5F, 0.5F, 0F));
            Vertices.Add(new Vector3(0.5F, -0.5F, 0F));
            Vertices.Add(new Vector3(-0.5F, -0.5F, 0F));
        }
    }
}
