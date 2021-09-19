using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugGLShapeAsset : BuilderAsset<DebugGLShape>
    {
        [SerializeField] private DebugGLMode m_mode = DebugGLMode.Line;
        [SerializeField] private Material m_material;

        public DebugGLMode Mode { get { return m_mode; } set { m_mode = value; } }
        public Material Material { get { return m_material; } set { m_material = value; } }
    }
}
