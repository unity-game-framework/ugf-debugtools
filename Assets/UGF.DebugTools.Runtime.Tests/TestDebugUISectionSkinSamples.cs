using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UGF.DebugTools.Runtime.UI.Scopes;
using UGF.DebugTools.Runtime.UI.Sections;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugUISectionSkinSamples : DebugUISection
    {
        private readonly Func<DebugUIMenu> m_onMenuCreate;
        private readonly DebugUIMenuItemHandler m_onMenuItemHandler;
        private readonly DebugUIMenuItemHandler m_onEnumItemHandler;
        private float m_slider;
        private string m_menu;
        private bool m_toggle;
        private string m_text = "Text";
        private string m_textArea = "Text Area";
        private int m_int = 10;
        private float m_float = 10.5F;
        private bool m_foldout;
        private string m_delayedText = "Delayed Text";
        private string m_delayedText2 = "Delayed Text 2";
        private TypeCode m_enum;

        public TestDebugUISectionSkinSamples() : base("UI Skin Samples")
        {
            m_onMenuCreate = OnMenuCreate;
            m_onMenuItemHandler = OnMenuSelect;
            m_onEnumItemHandler = OnEnumSelect;
        }

        protected override void OnDrawGUILayout()
        {
            DebugUI.Header("Basic");
            GUILayout.Button("Button");
            GUILayout.Label("Label");
            GUILayout.TextField("TextField");
            GUILayout.Box("Box");
            GUILayout.Toolbar(1, new[] { "Tab1", "Tab2" });
            GUILayout.Toggle(true, "Toggle");
            GUILayout.PasswordField("Password", '*');
            GUILayout.TextArea("TextArea");
            GUILayout.SelectionGrid(1, new[] { "Cell1", "Cell2", "Cell3", "Cell4" }, 2);
            m_slider = GUILayout.HorizontalSlider(m_slider, 0F, 1F);
            m_slider = GUILayout.VerticalSlider(m_slider, 0F, 1F);
            m_slider = GUILayout.HorizontalScrollbar(m_slider, 0.25F, 0F, 1F);
            m_slider = GUILayout.VerticalScrollbar(m_slider, 0.25F, 0F, 1F);

            DebugUI.Space();
            DebugUI.Header("Field");
            DebugUI.FieldLabel("Label", "Content");
            DebugUI.FieldButton("Button", "Content");
            DebugUI.FieldButton(string.Empty, "Content");
            DebugUI.FieldDropdown("Dropdown", m_menu, m_onMenuCreate);
            m_toggle = DebugUI.FieldToggle("Toggle", m_toggle);
            DebugUI.FieldPassword("Password", "Password");
            m_text = DebugUI.FieldText("Text", m_text);
            m_textArea = DebugUI.FieldTextArea("Text Area", m_textArea);
            m_int = DebugUI.FieldValue("Int", m_int);
            m_float = DebugUI.FieldValue("Float", m_float);
            DebugUI.FieldEnum("Enum", m_enum, m_onEnumItemHandler);

            DebugUI.Space();
            DebugUI.Header("Elements");
            GUILayout.Label("Enum");
            DebugUI.ValueEnum($"{m_enum}", m_enum, m_onEnumItemHandler);
            GUILayout.Label("Delayed Text Field");
            m_delayedText = DebugUI.DelayedTextField(m_delayedText);
            DebugUI.FieldLabel("Delayed Value", m_delayedText);
            m_delayedText2 = DebugUI.DelayedTextField(m_delayedText2);
            DebugUI.FieldLabel("Delayed Value 2", m_delayedText2);

            DebugUI.Space();
            DebugUI.Header("Menu");
            DebugUI.MenuDropdown("Menu Dropdown", m_onMenuCreate);

            if (DebugUI.FieldButton("Menu Context", m_menu))
            {
                DebugUI.MenuShowContext(OnMenuCreate());
            }

            DebugUI.Space();
            DebugUI.Header("Foldout");
            m_foldout = DebugUI.Foldout("Foldout", m_foldout);

            if (m_foldout)
            {
                using (new DebugUIIndentIncrementScope(1))
                {
                    for (int i = 0; i < 5; i++)
                    {
                        GUILayout.Button("Button");
                    }
                }
            }

            DebugUI.Space();
            DebugUI.Header("Indent");
            DebugUI.FieldButton("Button", "Content");

            using (new DebugUIIndentIncrementScope(1))
            {
                DebugUI.FieldButton("Button", "Content");

                using (new DebugUIIndentIncrementScope(1))
                {
                    DebugUI.FieldButton("Button", "Content");
                }
            }

            using (new DebugUIIndentLevelScope(3))
            {
                DebugUI.FieldButton("Button", "Content");
            }
        }

        private DebugUIMenu OnMenuCreate()
        {
            var menu = new DebugUIMenu();

            for (int i = 0; i < 5; i++)
            {
                string content = $"Item {i}";

                menu.Add(content, m_menu == content, m_onMenuItemHandler, content);
            }

            menu.AddDisabled("Disabled");

            return menu;
        }

        private void OnMenuSelect(DebugUIMenuItem item)
        {
            m_menu = item.GetValue<string>();
        }

        private void OnEnumSelect(DebugUIMenuItem item)
        {
            m_enum = item.GetValue<TypeCode>();
        }
    }
}
