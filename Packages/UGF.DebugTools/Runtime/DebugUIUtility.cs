using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUIUtility
    {
        public static Rect TransformToGUISpace(Rect rect)
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
