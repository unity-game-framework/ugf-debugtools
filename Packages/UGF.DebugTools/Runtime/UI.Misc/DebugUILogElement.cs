using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI.Misc
{
    public class DebugUILogElement : DebugUIElement
    {
        private readonly List<DebugUILogEntryData> m_entries = new List<DebugUILogEntryData>();
        private readonly ListView m_list;

        public DebugUILogElement()
        {
            var buttonClear = new Button
            {
                text = "Clear",
            };

            m_list = new ListView
            {
                itemsSource = m_entries,
                makeItem = OnListItemCreate,
                bindItem = OnListItemBind,
                unbindItem = OnListItemUnbind,
                virtualizationMethod = CollectionVirtualizationMethod.DynamicHeight
            };

            Add(buttonClear);
            Add(m_list);

            buttonClear.clicked += OnClearClicked;

            RegisterCallback<AttachToPanelEvent>(OnAttachToPanel);
            RegisterCallback<DetachFromPanelEvent>(OnDetachFromPanel);
        }

        private void OnAttachToPanel(AttachToPanelEvent _)
        {
            Application.logMessageReceived += OnMessageReceived;
        }

        private void OnDetachFromPanel(DetachFromPanelEvent _)
        {
            Application.logMessageReceived -= OnMessageReceived;
        }

        private VisualElement OnListItemCreate()
        {
            return new DebugUILogEntryElement();
        }

        private void OnListItemBind(VisualElement element, int index)
        {
            var entryElement = (DebugUILogEntryElement)element;

            entryElement.Bind(m_entries[index]);
        }

        private void OnListItemUnbind(VisualElement element, int index)
        {
            var entryElement = (DebugUILogEntryElement)element;

            entryElement.Unbind();
        }

        private void OnClearClicked()
        {
            m_entries.Clear();
            m_list.RefreshItems();
        }

        private void OnMessageReceived(string condition, string stacktrace, LogType type)
        {
            m_entries.Add(new DebugUILogEntryData(
                DateTime.Now,
                type,
                condition,
                stacktrace
            ));
        }
    }
}
