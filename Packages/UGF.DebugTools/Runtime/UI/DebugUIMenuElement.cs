using System;
using System.Collections.Generic;
using System.Linq;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI
{
    public class DebugUIMenuElement : DebugUIElement
    {
        private readonly IDictionary<GlobalId, DebugUIElement> m_menus;
        private readonly VisualElement m_headerContent;
        private readonly VisualElement m_bodyContent;
        private GlobalId? m_menuSelected;

        public DebugUIMenuElement(IDictionary<GlobalId, DebugUIElement> menus)
        {
            m_menus = menus ?? throw new ArgumentNullException(nameof(menus));

            var header = new VisualElement();

            m_headerContent = new VisualElement
            {
                visible = false
            };

            m_bodyContent = new VisualElement
            {
                visible = false,
            };

            Add(header);
            Add(m_bodyContent);

            var toggle = new Toggle();
            var selection = new PopupField<GlobalId>(menus.Keys.ToList(), 0, OnSelectionNameFormat, OnSelectionNameFormat);

            header.Add(toggle);
            header.Add(m_headerContent);

            m_headerContent.Add(new Label("Debug Menu"));
            m_bodyContent.Add(selection);

            foreach ((_, DebugUIElement element) in m_menus)
            {
                element.style.display = DisplayStyle.None;

                Add(element);
            }

            toggle.RegisterValueChangedCallback(OnToggle);
            selection.RegisterValueChangedCallback(OnSelectionChange);
        }

        private void OnToggle(ChangeEvent<bool> changeEvent)
        {
            m_headerContent.visible = changeEvent.newValue;
            m_bodyContent.visible = changeEvent.newValue;
        }

        private void OnSelectionChange(ChangeEvent<GlobalId> changeEvent)
        {
            if (m_menuSelected.HasValue)
            {
                m_menus[m_menuSelected.Value].style.display = DisplayStyle.None;
            }

            m_menuSelected = changeEvent.newValue;
            m_menus[m_menuSelected.Value].style.display = DisplayStyle.Flex;
        }

        private string OnSelectionNameFormat(GlobalId id)
        {
            return m_menus[id].name;
        }
    }
}
