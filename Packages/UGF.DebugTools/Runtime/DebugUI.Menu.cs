using System;
using UGF.DebugTools.Runtime.UI.Menu;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static bool MenuDropdown(string content, DebugUIMenu menu)
        {
            return MenuDropdown(DebugUIContentCache.GetContent(content), menu);
        }

        public static bool MenuDropdown(GUIContent content, DebugUIMenu menu)
        {
            return MenuDropdown(GetControlRect(), content, menu);
        }

        public static bool MenuDropdown(Rect position, GUIContent content, DebugUIMenu menu)
        {
            if (Dropdown(position, content))
            {
                MenuShowDropdown(menu, position);
                return true;
            }

            return false;
        }

        public static bool MenuDropdown(string content, Func<DebugUIMenu> onMenuCreate)
        {
            return MenuDropdown(DebugUIContentCache.GetContent(content), onMenuCreate);
        }

        public static bool MenuDropdown(GUIContent content, Func<DebugUIMenu> onMenuCreate)
        {
            return MenuDropdown(GetControlRect(), content, onMenuCreate);
        }

        public static bool MenuDropdown(Rect position, GUIContent content, Func<DebugUIMenu> onMenuCreate)
        {
            if (onMenuCreate == null) throw new ArgumentNullException(nameof(onMenuCreate));

            if (Dropdown(position, content))
            {
                DebugUIMenu menu = onMenuCreate();

                MenuShowDropdown(menu, position);
                return true;
            }

            return false;
        }

        public static void MenuShowDropdown(DebugUIMenu menu, Rect dropdownPosition)
        {
            Vector2 positionScreen = DebugUIUtility.GUIToScreenPosition(dropdownPosition.position);
            Rect position = DebugUIMenuUtility.GetMenuScreenPosition(new Vector2(positionScreen.x, positionScreen.y + dropdownPosition.height), dropdownPosition.width);

            MenuShow(menu, position);
        }

        public static void MenuShowContext(DebugUIMenu menu, float width = 150F)
        {
            Vector2 positionMouse = Event.current.mousePosition;
            Vector2 positionScreen = DebugUIUtility.GUIToScreenPosition(positionMouse);
            Rect position = DebugUIMenuUtility.GetMenuScreenPosition(positionScreen, width);

            MenuShow(menu, position);
        }

        public static void MenuShow(DebugUIMenu menu, Rect position)
        {
            if (menu == null) throw new ArgumentNullException(nameof(menu));

            DebugUIMenuDrawer drawer = GetMenuDrawer();

            if (drawer.HasMenu)
            {
                drawer.ClearMenu();
            }

            drawer.Position = position;
            drawer.SetMenu(menu);
            drawer.Focus();
        }

        public static DebugUIMenu GetMenuFromEnum(object value, DebugUIMenuItemHandler handler)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (handler == null) throw new ArgumentNullException(nameof(handler));

            Type type = value.GetType();
            string[] names = Enum.GetNames(type);
            Array values = Enum.GetValues(type);
            var menu = new DebugUIMenu();

            for (int i = 0; i < names.Length; i++)
            {
                string elementName = names[i];
                object elementValue = values.GetValue(i);
                var enumObject = (Enum)Enum.ToObject(type, elementValue);

                menu.Add(elementName, enumObject.Equals(value), handler, elementValue);
            }

            return menu;
        }

        private static DebugUIMenuDrawer GetMenuDrawer()
        {
            if (!Provider.Drawers.TryGet(out DebugUIMenuDrawer drawer))
            {
                drawer = new DebugUIMenuDrawer
                {
                    Display = true
                };

                Provider.Drawers.Add(GlobalId.Generate(), drawer);

                drawer.Initialize();
            }

            return drawer;
        }
    }
}
