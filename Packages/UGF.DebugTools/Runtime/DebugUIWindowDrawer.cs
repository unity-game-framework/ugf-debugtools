using System;
using UnityEditor;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIWindowDrawer : DebugUIDrawerBase
    {
        public bool Display { get; set; }
        public bool DisplayBackground { get; set; } = true;
        public Rect Position { get; set; } = new Rect(0F, 0F, 200F, 200F);
        public bool IsModal { get; set; }
        public int WindowId { get { return m_windowId ?? throw new ArgumentException("Value not specified."); } }

        private readonly GUI.WindowFunction m_windowFunction;
        private int? m_windowId;
        private Vector2 m_scroll;

        protected DebugUIWindowDrawer()
        {
            m_windowFunction = OnWindow;
        }

        public void Focus()
        {
            if (m_windowId.HasValue)
            {
                GUI.FocusWindow(m_windowId.Value);
            }
        }

        public void Unfocus()
        {
            GUI.UnfocusWindow();
        }

        protected override void OnDrawGUI()
        {
            if (Display)
            {
                OnUpdatePosition();

                m_windowId ??= GetHashCode();

                GUIStyle style = DisplayBackground ? GUI.skin.window : GUIStyle.none;

                Position = IsModal
                    ? GUI.ModalWindow(m_windowId.Value, Position, m_windowFunction, GUIContent.none, style)
                    : GUI.Window(m_windowId.Value, Position, m_windowFunction, GUIContent.none, style);
            }
        }

        protected virtual void OnUpdatePosition()
        {
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
