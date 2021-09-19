using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Shapes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug GL Quad Shape", order = 2000)]
    public class DebugGLShapeQuadAsset : DebugGLShapeAsset
    {
        protected override DebugGLShape OnBuild()
        {
            return new DebugGLShapeQuad(Mode, Material);
        }
    }
}
