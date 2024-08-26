using System;
using System.Collections;
using UnityEngine;

namespace UGF.DebugTools.Runtime.GL
{
    [AddComponentMenu("Unity Game Framework/Debug/Debug GL", 2000)]
    public class DebugGLComponent : MonoBehaviour
    {
        [SerializeField] private DebugGLProviderAsset m_provider;

        public DebugGLProviderAsset Provider { get { return m_provider; } set { m_provider = value; } }
        public DebugGLProvider Instance { get { return m_instance ?? throw new ArgumentException("Value not specified."); } }
        public bool HasInstance { get { return m_instance != null; } }

        private readonly Camera.CameraCallback m_onPostRenderHandler;
        private readonly WaitForEndOfFrame m_waitForEndOfFrame = new WaitForEndOfFrame();
        private DebugGLProvider m_instance;

        public DebugGLComponent()
        {
            m_onPostRenderHandler = OnRender;
        }

        private void Start()
        {
            if (DebugGL.HasProvider) throw new InvalidOperationException("Debug GL Drawer already specified.");

            m_instance = m_provider.Build();

            DebugGL.SetProvider(m_instance);

            m_instance.Initialize();

            Camera.onPostRender += m_onPostRenderHandler;

            StartCoroutine(OnRenderEndRoutine());
        }

        private void OnDestroy()
        {
            Camera.onPostRender -= m_onPostRenderHandler;

            if (HasInstance)
            {
                m_instance.Uninitialize();

                if (DebugGL.HasProvider && DebugGL.Provider == m_instance)
                {
                    DebugGL.ClearProvider();
                }

                m_instance = null;
            }
        }

        private void OnRender(Camera _)
        {
            if (HasInstance)
            {
                m_instance.DrawGL();
            }
        }

        private IEnumerator OnRenderEndRoutine()
        {
            while (enabled && HasInstance)
            {
                yield return m_waitForEndOfFrame;

                m_instance.Commands.Clear();
            }
        }
    }
}
