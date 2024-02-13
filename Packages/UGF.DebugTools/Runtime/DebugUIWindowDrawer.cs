using System;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIWindowDrawer : DebugUIDrawer
    {
        public bool Display { get; set; }
        public bool DisplayBackground { get; set; } = true;
        public bool Enabled { get; set; } = true;
        public Rect Position { get; set; } = new Rect(0F, 0F, 200F, 200F);
        public bool ScrollEnabled { get; set; } = true;
        public Vector2 Scroll { get; set; }
        public DebugUIWindowType WindowType { get; set; } = DebugUIWindowType.Default;
        public int WindowId { get { return m_windowId ?? throw new ArgumentException("Value not specified."); } }

        private readonly GUI.WindowFunction m_windowFunction;
        private int? m_windowId;

        protected DebugUIWindowDrawer()
        {
            m_windowFunction = OnWindow;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            m_windowId = GetHashCode();
        }

        public void Focus()
        {
            if (m_windowId.HasValue)
            {
                GUI.FocusWindow(m_windowId.Value);
            }
        }

        public void UnFocus()
        {
            GUI.UnfocusWindow();
        }

        public void SetSize(Vector2 size)
        {
            Rect position = Position;

            position.size = size;

            Position = position;
        }

        protected override void OnDrawGUI()
        {
            if (Display)
            {
                OnUpdatePosition();

                GUIStyle style = DisplayBackground ? GUI.skin.window : GUIStyle.none;

                using (new DebugUIEnabledScope(Enabled))
                {
                    switch (WindowType)
                    {
                        case DebugUIWindowType.Default:
                        {
                            Position = GUI.Window(WindowId, Position, m_windowFunction, GUIContent.none, style);
                            break;
                        }
                        case DebugUIWindowType.Modal:
                        {
                            Position = GUI.ModalWindow(WindowId, Position, m_windowFunction, GUIContent.none, style);
                            break;
                        }
                        case DebugUIWindowType.Layout:
                        {
                            Position = GUILayout.Window(WindowId, Position, m_windowFunction, GUIContent.none, style);
                            break;
                        }
                        case DebugUIWindowType.Area:
                        {
                            using (new DebugUILayoutAreaScope(Position, GUIContent.none, style))
                            {
                                OnWindow(WindowId);
                            }

                            break;
                        }
                        default:
                        {
                            throw new ArgumentOutOfRangeException(nameof(WindowType), WindowType, "Debug UI window type is unknown.");
                        }
                    }
                }
            }
        }

        protected virtual void OnUpdatePosition()
        {
        }

        protected abstract void OnDrawGUILayout();

        private void OnWindow(int _)
        {
            if (ScrollEnabled)
            {
                using (var view = new DebugUIScrollViewScope(Scroll))
                {
                    OnDrawGUILayout();

                    Scroll = view.ScrollPosition;
                }
            }
            else
            {
                OnDrawGUILayout();
            }
        }
    }
}
