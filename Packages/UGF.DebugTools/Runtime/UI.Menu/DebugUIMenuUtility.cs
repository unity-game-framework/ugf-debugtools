using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public static class DebugUIMenuUtility
    {
        public static Rect GetMenuScreenPosition(Vector2 position, float width)
        {
            Rect screen = DebugUIUtility.GetScreenRect();
            float height = screen.max.y - position.y;

            return new Rect(position.x, position.y, width, height);
        }
    }
}
