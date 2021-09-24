using System;
using System.Collections;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.IMGUI.AssetReferences;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [AddComponentMenu("Unity Game Framework/Debug/Debug GL", 2000)]
    public class DebugGLComponent : MonoBehaviour
    {
        [SerializeField] private bool m_enable = true;
        [SerializeField] private bool m_defaultShapes = true;
        [SerializeField] private Material m_defaultMaterial;
        [SerializeField] private List<AssetReference<DebugGLShapeAsset>> m_shapes = new List<AssetReference<DebugGLShapeAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public bool DefaultShapes { get { return m_defaultShapes; } set { m_defaultShapes = value; } }
        public Material DefaultMaterial { get { return m_defaultMaterial; } set { m_defaultMaterial = value; } }
        public List<AssetReference<DebugGLShapeAsset>> Shapes { get { return m_shapes; } }
        public DebugGLDrawer Drawer { get { return m_drawer ?? throw new ArgumentException("Value not specified."); } }
        public bool HasDrawer { get { return m_drawer != null; } }

        private DebugGLDrawer m_drawer;
        private readonly Camera.CameraCallback m_onPostRenderHandler;
        private static readonly WaitForEndOfFrame m_waitForEndOfFrame = new WaitForEndOfFrame();

        public DebugGLComponent()
        {
            m_onPostRenderHandler = OnRender;
        }

        private void Start()
        {
            if (DebugGL.HasDrawer) throw new InvalidOperationException("Debug GL Drawer already specified.");

            m_drawer = new DebugGLDrawer
            {
                Enable = m_enable
            };

            m_drawer.SetDefaultMaterial(m_defaultMaterial ? m_defaultMaterial : DebugGLUtility.CreateDefaultMaterial());

            if (m_defaultShapes)
            {
                m_drawer.AddShape(DebugGL.ShapeLineWireId, DebugGLUtility.CreateShapeLineWire());
                m_drawer.AddShape(DebugGL.ShapeTriangleWireId, DebugGLUtility.CreateShapeTriangleWire());
                m_drawer.AddShape(DebugGL.ShapeQuadWireId, DebugGLUtility.CreateShapeQuadWire());
                m_drawer.AddShape(DebugGL.ShapeCircleWireId, DebugGLUtility.CreateShapeCircleWire());
                m_drawer.AddShape(DebugGL.ShapeCubeWireId, DebugGLUtility.CreateShapeCubeWire());
                m_drawer.AddShape(DebugGL.ShapeSphereWireId, DebugGLUtility.CreateShapeSphereWire());
                m_drawer.AddShape(DebugGL.ShapeCylinderWireId, DebugGLUtility.CreateShapeCylinderWire());
            }

            for (int i = 0; i < m_shapes.Count; i++)
            {
                AssetReference<DebugGLShapeAsset> reference = m_shapes[i];

                m_drawer.AddShape(reference.Guid, reference.Asset.Build());
            }

            DebugGL.DrawerSet(m_drawer);

            m_drawer.Initialize();
        }

        private void OnDestroy()
        {
            if (HasDrawer)
            {
                m_drawer.Uninitialize();

                if (DebugGL.HasDrawer && DebugGL.Drawer == m_drawer)
                {
                    DebugGL.DrawerClear();
                }

                m_drawer = null;
            }
        }

        private void OnEnable()
        {
            Camera.onPostRender += m_onPostRenderHandler;

            StartCoroutine(OnRenderEndRoutine());
        }

        private void OnDisable()
        {
            Camera.onPostRender -= m_onPostRenderHandler;
        }

        private void OnRender(Camera _)
        {
            if (HasDrawer)
            {
                m_drawer.DrawGL();
            }
        }

        private IEnumerator OnRenderEndRoutine()
        {
            while (enabled && HasDrawer)
            {
                yield return m_waitForEndOfFrame;

                m_drawer.ClearCommands();
            }
        }
    }
}
