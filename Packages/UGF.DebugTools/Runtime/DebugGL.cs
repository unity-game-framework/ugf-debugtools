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
            Drawer.AddShape(ShapeCubeWireId, CreateCubeWire(DebugGLUtility.DefaultMaterial));

            for (int i = 0; i < settings.Shapes.Count; i++)
            {
                AssetReference<DebugGLShapeAsset> reference = settings.Shapes[i];

                Drawer.AddShape(reference.Guid, reference.Asset.Build());
            }

            Camera.onPostRender += OnDrawGL;
        }

        private static void OnDrawGL(Camera camera)
        {
            Drawer.DrawGL();
        }

        [RuntimeInitializeOnLoadMethod]
        private static void OnInitialize()
        {
        }
    }
}
