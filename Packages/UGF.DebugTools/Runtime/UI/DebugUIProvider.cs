using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace UGF.DebugTools.Runtime.UI
{
    public class DebugUIProvider : InitializeBase
    {
        public PanelSettings PanelSettings { get; }
        public string DocumentGameObjectName { get; }
        public Provider<GlobalId, DebugUIElement> Elements { get; } = new Provider<GlobalId, DebugUIElement>();
        public UIDocument Document { get { return m_document ?? throw new InitializeStateException(); } }

        private UIDocument m_document;

        public DebugUIProvider(PanelSettings panelSettings, string documentGameObjectName = "DebugUIDocument")
        {
            if (string.IsNullOrEmpty(documentGameObjectName)) throw new ArgumentException("Value cannot be null or empty.", nameof(documentGameObjectName));

            PanelSettings = panelSettings;
            DocumentGameObjectName = documentGameObjectName;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_document = new GameObject(DocumentGameObjectName).AddComponent<UIDocument>();
            m_document.panelSettings = PanelSettings;

            foreach ((_, DebugUIElement element) in Elements)
            {
                m_document.rootVisualElement.Add(element);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            if (m_document != null)
            {
                foreach ((_, DebugUIElement element) in Elements)
                {
                    m_document.rootVisualElement.Remove(element);
                }

                Object.Destroy(m_document.gameObject);
            }

            Elements.Clear();

            m_document = null;
        }
    }
}
