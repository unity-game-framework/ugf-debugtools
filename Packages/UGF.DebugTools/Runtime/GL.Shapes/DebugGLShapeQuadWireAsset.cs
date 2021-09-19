using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Shapes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug GL Quad Wire Shape", order = 2000)]
    public class DebugGLShapeQuadWireAsset : DebugGLShapeAsset
    {
        protected override DebugGLShape OnBuild()
        {
            return new DebugGLShapeQuadWire();
        }
    }
}
