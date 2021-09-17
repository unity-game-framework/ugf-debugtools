using System;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUIUtility
    {
        public static bool TryWorldToGUIPosition(Vector3 position, out Vector2 result)
        {
            return TryWorldToGUIPosition(position, Camera.current, out result);
        }

        public static bool TryWorldToGUIPosition(Vector3 position, Camera camera, out Vector2 result)
        {
            if (camera == null) throw new ArgumentNullException(nameof(camera));

            Vector3 positionScreen = camera.WorldToScreenPoint(position);

            if (positionScreen.z > 0F)
            {
                Vector2 positionGUI = GUIUtility.ScreenToGUIPoint(positionScreen);
                Rect screen = GetScreenRect();

                result = new Vector2(positionGUI.x, screen.height - positionGUI.y);
                return true;
            }

            result = default;
            return false;
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

        public static Vector2 GUIToScreenPosition(Vector2 position)
        {
            using (new DebugUIMatrixScope(Matrix4x4.identity))
            {
                return GUIUtility.GUIToScreenPoint(position);
            }
        }
    }
}
