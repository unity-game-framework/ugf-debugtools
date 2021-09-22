using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugGL
    {
        public static DebugGLDrawer Drawer { get; } = new DebugGLDrawer();
        public static Material DefaultMaterial { get { return m_defaultMaterial != null ? m_defaultMaterial : throw new ArgumentException("Value not specified."); } }
        public static bool HasDefaultMaterial { get { return m_defaultMaterial != null; } }

        private static Material m_defaultMaterial;

        static DebugGL()
        {
            Drawer.AddShape(ShapeLineWireId, DebugGLUtility.CreateShapeLineWire());
            Drawer.AddShape(ShapeTriangleWireId, DebugGLUtility.CreateShapeTriangleWire());
            Drawer.AddShape(ShapeQuadWireId, DebugGLUtility.CreateShapeQuadWire());
            Drawer.AddShape(ShapeCircleWireId, DebugGLUtility.CreateShapeCircleWire());
            Drawer.AddShape(ShapeCubeWireId, DebugGLUtility.CreateShapeCubeWire());
            Drawer.AddShape(ShapeSphereWireId, DebugGLUtility.CreateShapeSphereWire());
            Drawer.AddShape(ShapeCylinderWireId, DebugGLUtility.CreateShapeCylinderWire());
        }

        public static void SetDefaultMaterial(Material material)
        {
            if (material == null) throw new ArgumentNullException(nameof(material));

            m_defaultMaterial = material;
        }

        public static void ClearDefaultMaterial()
        {
            m_defaultMaterial = null;
        }
    }
}
