using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public static class DebugUIMenuUtility
    {
        public static Rect GetMenuScreenPosition(Vector2 position, float width)
        {
            Rect screen = DebugUIUtility.GetScreenRect();
            float height = screen.max.y - position.y;
            bool isPositionOutOfScreen = position.x + width > screen.max.x;
            float horizontalOffset = isPositionOutOfScreen ? width - (screen.max.x - position.x) : 0F;

            return new Rect(position.x - horizontalOffset, position.y, width, height);
        }
    }
}
