using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Shapes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug GL Quad Shape", order = 2000)]
    public class DebugGLShapeQuadAsset : DebugGLShapeAsset
    {
        protected override DebugGLShape OnBuild()
        {
            DebugGLShape shape = base.OnBuild();

            shape.Vertices.Add(new Vector3(-0.5F, -0.5F, 0F));
            shape.Vertices.Add(new Vector3(-0.5F, 0.5F, 0F));
            shape.Vertices.Add(new Vector3(0.5F, 0.5F, 0F));
            shape.Vertices.Add(new Vector3(0.5F, -0.5F, 0F));
            shape.Vertices.Add(new Vector3(-0.5F, -0.5F, 0F));

            return shape;
        }
    }
}
