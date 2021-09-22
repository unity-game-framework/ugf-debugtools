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
        [SerializeField] private Material m_defaultMaterial;
        [SerializeField] private List<AssetReference<DebugGLShapeAsset>> m_shapes = new List<AssetReference<DebugGLShapeAsset>>();

        public bool Enable { get { return m_enable; } set { m_enable = value; } }
        public Material DefaultMaterial { get { return m_defaultMaterial; } set { m_defaultMaterial = value; } }
        public List<AssetReference<DebugGLShapeAsset>> Shapes { get { return m_shapes; } }

        private static readonly WaitForEndOfFrame m_waitForEndOfFrame = new WaitForEndOfFrame();
        private static readonly Camera.CameraCallback m_onPostRenderHandler = OnRender;

        private void Start()
        {
            DebugGL.Drawer.Enable = m_enable;
            DebugGL.SetDefaultMaterial(m_defaultMaterial ? m_defaultMaterial : DebugGLUtility.CreateDefaultMaterial());

            for (int i = 0; i < m_shapes.Count; i++)
            {
                AssetReference<DebugGLShapeAsset> reference = m_shapes[i];

                DebugGL.Drawer.AddShape(reference.Guid, reference.Asset.Build());
            }
        }

        private void OnDestroy()
        {
            DebugGL.ClearDefaultMaterial();

            for (int i = 0; i < m_shapes.Count; i++)
            {
                AssetReference<DebugGLShapeAsset> reference = m_shapes[i];

                DebugGL.Drawer.RemoveShape(reference.Guid);
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

        private static void OnRender(Camera _)
        {
            DebugGL.Drawer.DrawGL();
        }

        private IEnumerator OnRenderEndRoutine()
        {
            while (enabled)
            {
                yield return m_waitForEndOfFrame;

                DebugGL.Drawer.ClearCommands();
            }
        }
    }
}
