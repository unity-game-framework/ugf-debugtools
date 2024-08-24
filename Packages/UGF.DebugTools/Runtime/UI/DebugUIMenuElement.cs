using System;
using System.Collections.Generic;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI
{
    public class DebugUIMenuElement : DebugUIElement
    {
        public string DisplayName { get; }
        public IDictionary<GlobalId, DebugUIElement> Menus { get; }
        public GlobalId Selected { get { return m_selected ?? throw new ArgumentException("Value not specified."); } }
        public bool HasSelected { get { return m_selected != null; } }

        public static string UssClassName { get; } = "ugf-debugtools-menu";
        public static string EnabledUssClassName { get; } = "ugf-debugtools-menu--enabled";
        public static string DisabledUssClassName { get; } = "ugf-debugtools-menu--disabled";
        public static string HeaderUssClassName { get; } = "ugf-debugtools-menu__header";
        public static string HeaderContentUssClassName { get; } = "ugf-debugtools-menu__header-content";
        public static string BodyUssClassName { get; } = "ugf-debugtools-menu__body";
        public static string BodyContentUssClassName { get; } = "ugf-debugtools-menu__body-content";
        public static string BodyContentSelectedUssClassName { get; } = "ugf-debugtools-menu__body-content--selected";
        public static string BodyContentNotSelectedUssClassName { get; } = "ugf-debugtools-menu__body-content--not-selected";

        private readonly VisualElement m_headerContent;
        private readonly VisualElement m_bodyContent;
        private GlobalId? m_selected;

        public DebugUIMenuElement(string displayName, IDictionary<GlobalId, DebugUIElement> menus)
        {
            if (string.IsNullOrEmpty(displayName)) throw new ArgumentException("Value cannot be null or empty.", nameof(displayName));

            DisplayName = displayName;
            Menus = menus ?? throw new ArgumentNullException(nameof(menus));

            var header = new VisualElement();
            var body = new VisualElement();

            m_headerContent = new VisualElement
            {
                visible = false
            };

            m_bodyContent = new VisualElement
            {
                visible = false,
            };

            var toggle = new Toggle();
            var selection = new PopupField<GlobalId?>(OnSelectionCreateList(), 0, OnSelectionNameFormat, OnSelectionNameFormat);

            Add(header);
            Add(body);

            header.Add(toggle);
            header.Add(m_headerContent);
            body.Add(m_bodyContent);

            m_headerContent.Add(new Label(DisplayName));
            m_bodyContent.Add(selection);

            foreach ((_, DebugUIElement element) in Menus)
            {
                element.AddToClassList(BodyContentNotSelectedUssClassName);

                m_bodyContent.Add(element);
            }

            toggle.RegisterValueChangedCallback(OnToggle);
            selection.RegisterValueChangedCallback(OnSelectionChange);

            AddToClassList(UssClassName);
            AddToClassList(DisabledUssClassName);

            header.AddToClassList(HeaderUssClassName);
            body.AddToClassList(BodyUssClassName);

            m_headerContent.AddToClassList(HeaderContentUssClassName);
            m_bodyContent.AddToClassList(BodyContentUssClassName);
        }

        public void SetSelected(GlobalId id)
        {
            if (!id.IsValid()) throw new ArgumentException("Value should be valid.", nameof(id));

            ClearSelected();

            m_selected = id;

            DebugUIElement element = Menus[m_selected.Value];

            element.RemoveFromClassList(BodyContentNotSelectedUssClassName);
            element.AddToClassList(BodyContentSelectedUssClassName);
        }

        public bool ClearSelected()
        {
            if (m_selected.HasValue)
            {
                DebugUIElement element = Menus[m_selected.Value];

                element.RemoveFromClassList(BodyContentSelectedUssClassName);
                element.AddToClassList(BodyContentNotSelectedUssClassName);

                m_selected = null;
                return true;
            }

            return false;
        }

        private void OnToggle(ChangeEvent<bool> changeEvent)
        {
            EnableInClassList(EnabledUssClassName, changeEvent.newValue);
            EnableInClassList(DisabledUssClassName, !changeEvent.newValue);

            m_headerContent.visible = changeEvent.newValue;
            m_bodyContent.visible = changeEvent.newValue;
        }

        private void OnSelectionChange(ChangeEvent<GlobalId?> changeEvent)
        {
            if (changeEvent.newValue.HasValue)
            {
                SetSelected(changeEvent.newValue.Value);
            }
            else
            {
                ClearSelected();
            }
        }

        private string OnSelectionNameFormat(GlobalId? id)
        {
            return id.HasValue ? Menus[id.Value].name : "None";
        }

        private List<GlobalId?> OnSelectionCreateList()
        {
            var list = new List<GlobalId?> { default };

            foreach ((GlobalId id, _) in Menus)
            {
                list.Add(id);
            }

            return list;
        }
    }
}
