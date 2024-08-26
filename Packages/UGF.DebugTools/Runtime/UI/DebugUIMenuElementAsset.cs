using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Menu Element", order = 2000)]
    public class DebugUIMenuElementAsset : DebugUIElementAsset
    {
        [SerializeField] private string m_displayName = "Debug Menu";
        [SerializeField] private List<MenuData> m_menu = new List<MenuData>();

        public string DisplayName { get { return m_displayName; } set { m_displayName = value; } }
        public List<MenuData> Menu { get { return m_menu; } }

        [Serializable]
        public struct MenuData
        {
            [SerializeField] private string m_name;
            [SerializeField] private DebugUIElementAsset m_element;

            public string Name { get { return m_name; } set { m_name = value; } }
            public DebugUIElementAsset Element { get { return m_element; } set { m_element = value; } }
        }

        protected override DebugUIElement OnBuild()
        {
            var menus = new List<DebugUIMenu>();

            for (int i = 0; i < m_menu.Count; i++)
            {
                MenuData menu = m_menu[i];

                menus.Add(new DebugUIMenu(menu.Name, menu.Element.Build()));
            }

            return new DebugUIMenuElement(m_displayName, menus);
        }
    }
}
