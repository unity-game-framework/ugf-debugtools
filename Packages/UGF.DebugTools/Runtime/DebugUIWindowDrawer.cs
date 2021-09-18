using UnityEditor;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIWindowDrawer : DebugUIDrawerBase
    {
        public bool Display { get; set; }
        public bool DisplayBackground { get; set; } = true;

        private readonly GUI.WindowFunction m_windowFunction;
        private int? m_windowId;
        private Vector2 m_scroll;

        protected DebugUIWindowDrawer()
        {
            m_windowFunction = OnWindow;
        }

        protected override void OnDrawGUI()
        {
            if (Display)
            {
                Rect position = OnGetPosition();
                m_windowId ??= GetHashCode();

                GUIStyle style = DisplayBackground ? GUI.skin.window : GUIStyle.none;

                GUI.Window(m_windowId.Value, position, m_windowFunction, GUIContent.none, style);
            }
        }

        protected virtual Rect OnGetPosition()
        {
            return DebugUIUtility.GetScreenRect();
        }

        protected abstract void OnDrawGUILayout();

        private void OnWindow(int id)
        {
            using (var view = new EditorGUILayout.ScrollViewScope(m_scroll))
            {
                OnDrawGUILayout();

                m_scroll = view.scrollPosition;
            }
        }
    }
}
