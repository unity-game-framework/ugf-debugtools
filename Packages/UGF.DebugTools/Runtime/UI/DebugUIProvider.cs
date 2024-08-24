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
        public Provider<GlobalId, DebugUIElement> Drawers { get; } = new Provider<GlobalId, DebugUIElement>();
        public Provider<GlobalId, VisualElement> Elements { get; } = new Provider<GlobalId, VisualElement>();

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

            foreach ((GlobalId id, DebugUIElement drawer) in Drawers)
            {
                drawer.Initialize();

                VisualElement element = drawer.CreateElement();

                Elements.Add(id, element);

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
            foreach ((GlobalId id, DebugUIElement drawer) in Drawers)
            {
                drawer.Uninitialize();

                VisualElement element = Elements.Get(id);

                Document.rootVisualElement.Remove(element);
            }

            Drawers.Clear();
            Elements.Clear();
        }
    }
}
