using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI
{
    public class DebugUIMenuElement : DebugUIElement
    {
        public string DisplayName { get; }
        public IReadOnlyList<DebugUIMenu> Menus { get; }
        public int Selected { get { return m_selected ?? throw new ArgumentException("Value not specified."); } }
        public bool HasSelected { get { return m_selected.HasValue; } }

        public static string NoneMenuName { get; } = "None";
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
        private int? m_selected;

        public DebugUIMenuElement(string displayName, IReadOnlyList<DebugUIMenu> menus)
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
            var selection = new PopupField<int?>(OnSelectionCreateList(), 0, OnSelectionNameFormat, OnSelectionNameFormat);

            Add(header);
            Add(body);

            header.Add(toggle);
            header.Add(m_headerContent);
            body.Add(m_bodyContent);

            m_headerContent.Add(new Label(DisplayName));
            m_bodyContent.Add(selection);

            for (int i = 0; i < Menus.Count; i++)
            {
                DebugUIMenu menu = Menus[i];

                menu.Element.AddToClassList(BodyContentNotSelectedUssClassName);

                m_bodyContent.Add(menu.Element);
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

        public void SetSelected(int index)
        {
            if (index < 0 || index >= Menus.Count) throw new ArgumentOutOfRangeException(nameof(index));

            ClearSelected();

            m_selected = index;

            DebugUIMenu menu = Menus[m_selected.Value];

            menu.Element.RemoveFromClassList(BodyContentNotSelectedUssClassName);
            menu.Element.AddToClassList(BodyContentSelectedUssClassName);
        }

        public bool ClearSelected()
        {
            if (m_selected.HasValue)
            {
                DebugUIMenu menu = Menus[m_selected.Value];

                menu.Element.RemoveFromClassList(BodyContentSelectedUssClassName);
                menu.Element.AddToClassList(BodyContentNotSelectedUssClassName);

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

        private void OnSelectionChange(ChangeEvent<int?> changeEvent)
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

        private string OnSelectionNameFormat(int? id)
        {
            return id.HasValue ? Menus[id.Value].Name : NoneMenuName;
        }

        private List<int?> OnSelectionCreateList()
        {
            var list = new List<int?> { default };

            for (int i = 0; i < Menus.Count; i++)
            {
                list.Add(i);
            }

            return list;
        }
    }
}
