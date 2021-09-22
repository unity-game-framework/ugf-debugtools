using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugGL
    {
        public static DebugGLDrawer Drawer { get; } = new DebugGLDrawer();

        static DebugGL()
        {
            DebugGLSettingsAsset settings = DebugGLSettings.Settings.GetData();

            Drawer.Enable = settings.Enable;
            Drawer.AddShape(ShapeLineWireId, DebugGLUtility.CreateShapeLineWire());
            Drawer.AddShape(ShapeTriangleWireId, DebugGLUtility.CreateShapeTriangleWire());
            Drawer.AddShape(ShapeQuadWireId, DebugGLUtility.CreateShapeQuadWire());
            Drawer.AddShape(ShapeCircleWireId, DebugGLUtility.CreateShapeCircleWire());
            Drawer.AddShape(ShapeCubeWireId, DebugGLUtility.CreateShapeCubeWire());
            Drawer.AddShape(ShapeSphereWireId, DebugGLUtility.CreateShapeSphereWire());
            Drawer.AddShape(ShapeCylinderWireId, DebugGLUtility.CreateShapeCylinderWire());

            for (int i = 0; i < settings.Shapes.Count; i++)
            {
                AssetReference<DebugGLShapeAsset> reference = settings.Shapes[i];

                Drawer.AddShape(reference.Guid, reference.Asset.Build());
            }

            Camera.onPostRender += OnDrawGL;
        }

        public static Material GetDefaultMaterial()
        {
            DebugGLSettingsAsset settings = DebugGLSettings.Settings.GetData();

            return settings.DefaultMaterial ? settings.DefaultMaterial : DebugGLUtility.DefaultMaterial;
        }

        private static void OnDrawGL(Camera camera)
        {
            Drawer.DrawGL();
        }
    }
}
