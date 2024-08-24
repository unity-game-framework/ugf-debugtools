using System;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI
{
    public class DebugUIProvider : InitializeBase
    {
        public UIDocument Document { get; }
        public Provider<GlobalId, DebugUIElement> Elements { get; } = new Provider<GlobalId, DebugUIElement>();

        public DebugUIProvider(PanelSettings panelSettings, string documentGameObjectName = "DebugUIDocument") : this(DebugUIUtility.CreateDocument(panelSettings, documentGameObjectName))
        {
        }

        public DebugUIProvider(UIDocument document)
        {
            Document = document ?? throw new ArgumentNullException(nameof(document));
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            foreach ((_, DebugUIElement element) in Elements)
            {
                Document.rootVisualElement.Add(element);
            }
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Clear();
        }

        public void Clear()
        {
            foreach ((_, DebugUIElement element) in Elements)
            {
                Document.rootVisualElement.Remove(element);
            }

            Elements.Clear();
        }
    }
}
