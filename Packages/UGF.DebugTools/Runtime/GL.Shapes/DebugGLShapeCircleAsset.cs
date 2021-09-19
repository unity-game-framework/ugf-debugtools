using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Shapes
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug GL Circle Shape", order = 2000)]
    public class DebugGLShapeCircleAsset : DebugGLShapeAsset
    {
        [SerializeField] private int m_segments = 8;
        [SerializeField] private float m_degrees = 360F;

        public int Segments { get { return m_segments; } set { m_segments = value; } }
        public float Degrees { get { return m_degrees; } set { m_degrees = value; } }

        protected override DebugGLShape OnBuild()
        {
            return new DebugGLShapeCircle(m_segments, m_degrees, Mode, Material);
        }
    }
}
