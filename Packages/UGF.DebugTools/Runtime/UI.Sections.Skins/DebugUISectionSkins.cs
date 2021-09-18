using System;
using System.Collections.Generic;
using UGF.DebugTools.Runtime.UI.Menu;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Skins
{
    public class DebugUISectionSkins : DebugUISection
    {
        public List<GUISkin> Skins { get; } = new List<GUISkin>();

        private readonly Func<DebugUIMenu> m_onMenuSkinCreate;

        public DebugUISectionSkins() : base("UI Skins")
        {
            m_onMenuSkinCreate = OnMenuSkinCreate;
        }

        protected override void OnDrawGUILayout()
        {
            DebugUI.FieldDropdown("Skin", GUI.skin.name, m_onMenuSkinCreate);
        }

        private DebugUIMenu OnMenuSkinCreate()
        {
            var menu = new DebugUIMenu();

            for (int i = 0; i < Skins.Count; i++)
            {
                GUISkin skin = Skins[i];

                menu.Add(new GUIContent(skin.name), GUI.skin == skin, item => DebugUI.Drawer.SetSkin(item.GetValue<GUISkin>()), skin);
            }

            return menu;
        }
    }
}
