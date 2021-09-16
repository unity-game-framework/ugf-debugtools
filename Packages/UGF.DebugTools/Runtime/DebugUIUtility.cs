using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUIUtility
    {
        public static Vector2 WorldToScreenPoint(Vector3 position)
        {
        }

        public static Rect GetScreenRect()
        {
            var position = new Rect(0F, 0F, Screen.width, Screen.height);

            position = ScaleToGUISpace(position);

            return position;
        }

        public static Rect ScaleToGUISpace(Rect rect)
        {
            Matrix4x4 matrix = GUI.matrix.inverse;
            Vector2 position = rect.min;
            Vector2 size = rect.size;

            position = matrix.MultiplyPoint(position);
            size = matrix.MultiplyPoint(size);

            return new Rect(position, size);
        }
    }
}
