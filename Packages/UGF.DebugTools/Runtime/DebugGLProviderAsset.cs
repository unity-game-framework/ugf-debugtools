using System.Collections.Generic;
using UGF.Builder.Runtime;
using UGF.EditorTools.Runtime.Assets;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug GL Provider", order = 2000)]
    public class DebugGLProviderAsset : BuilderAsset<DebugGLProvider>
    {
        [SerializeField] private bool m_enable = true;
        [SerializeField] private bool m_defaultShapes = true;
        [SerializeField] private Material m_defaultMaterial;
        [SerializeField] private List<AssetIdReference<DebugGLShapeAsset>> m_shapes = new List<AssetIdReference<DebugGLShapeAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public bool DefaultShapes { get { return m_defaultShapes; } set { m_defaultShapes = value; } }
        public Material DefaultMaterial { get { return m_defaultMaterial; } set { m_defaultMaterial = value; } }
        public List<AssetIdReference<DebugGLShapeAsset>> Shapes { get { return m_shapes; } }

        protected override DebugGLProvider OnBuild()
        {
            var provider = new DebugGLProvider
            {
                Enable = m_enable
            };

            provider.SetDefaultMaterial(m_defaultMaterial ? m_defaultMaterial : DebugGLUtility.CreateDefaultMaterial());

            if (m_defaultShapes)
            {
                provider.Shapes.Add(DebugGL.ShapeLineWireId, DebugGLUtility.CreateShapeLineWire());
                provider.Shapes.Add(DebugGL.ShapeTriangleWireId, DebugGLUtility.CreateShapeTriangleWire());
                provider.Shapes.Add(DebugGL.ShapeQuadWireId, DebugGLUtility.CreateShapeQuadWire());
                provider.Shapes.Add(DebugGL.ShapeCircleWireId, DebugGLUtility.CreateShapeCircleWire());
                provider.Shapes.Add(DebugGL.ShapeCubeWireId, DebugGLUtility.CreateShapeCubeWire());
                provider.Shapes.Add(DebugGL.ShapeSphereWireId, DebugGLUtility.CreateShapeSphereWire());
                provider.Shapes.Add(DebugGL.ShapeCylinderWireId, DebugGLUtility.CreateShapeCylinderWire());
            }

            for (int i = 0; i < m_shapes.Count; i++)
            {
                AssetIdReference<DebugGLShapeAsset> reference = m_shapes[i];

                provider.Shapes.Add(reference.Guid, reference.Asset.Build());
            }

            return provider;
        }
    }
}
