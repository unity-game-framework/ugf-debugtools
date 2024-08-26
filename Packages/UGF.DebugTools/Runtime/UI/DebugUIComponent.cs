using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI
{
    [AddComponentMenu("Unity Game Framework/Debug/Debug UI", 2000)]
    public class DebugUIComponent : MonoBehaviour
    {
        [SerializeField] private DebugUIProviderAsset m_provider;

        public DebugUIProviderAsset Provider { get { return m_provider; } set { m_provider = value; } }
        public DebugUIProvider Instance { get { return m_instance ?? throw new ArgumentException("Value not specified."); } }
        public bool HasInstance { get { return m_instance != null; } }

        private DebugUIProvider m_instance;

        private void Start()
        {
            if (DebugUI.HasProvider) throw new InvalidOperationException("Debug UI Drawer already specified.");

            m_instance = m_provider.Build();

            DebugUI.SetProvider(m_instance);

            m_instance.Initialize();
        }

        private void OnDestroy()
        {
            if (HasInstance)
            {
                m_instance.Uninitialize();

                if (DebugUI.HasProvider && DebugUI.Provider == m_instance)
                {
                    DebugUI.ClearProvider();
                }

                m_instance = null;
            }
        }
    }
}
