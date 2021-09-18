using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public abstract class DebugUIPanel
    {
        public bool Display { get; set; } = true;
        public bool DisplayBackground { get; set; }
        public Vector3 Position { get; set; }
        public Vector2 Size { get; set; } = Vector2.one * 250F;
        public Rect Rect { get; private set; }
        public bool IsVisible { get; private set; }
        public object BindTarget { get { return m_bindTarget ?? throw new ArgumentException("Value not specified."); } }
        public bool HasBindTarget { get { return m_bindTarget != null; } }
        public DebugUIPanelBindHandler BindHandler { get { return m_bindHandler ?? throw new ArgumentException("Value not specified."); } }
        public bool HasBindHandler { get { return m_bindHandler != null; } }

        private readonly GUI.WindowFunction m_windowFunction;
        private object m_bindTarget;
        private DebugUIPanelBindHandler m_bindHandler;
        private int? m_windowId;

        protected DebugUIPanel()
        {
            m_windowFunction = OnWindow;
        }

        public void Enable()
        {
            OnEnable();
        }

        public void Disable()
        {
            OnDisable();
        }

        public void DrawGUI()
        {
            if (Display)
            {
                if (IsVisible)
                {
                    m_windowId ??= GetHashCode();

                    GUIStyle style = DisplayBackground ? GUI.skin.window : GUIStyle.none;

                    GUI.Window(m_windowId.Value, Rect, m_windowFunction, GUIContent.none, style);
                }

                if (Event.current.type == EventType.Repaint)
                {
                    if (HasBindTarget)
                    {
                        Position = m_bindHandler.Invoke(m_bindTarget);
                    }

                    if (DebugUIUtility.TryWorldToGUIPosition(Position, out Vector2 position))
                    {
                        Rect screen = DebugUIUtility.GetScreenRect();

                        Rect = new Rect(position, Size);
                        IsVisible = screen.Contains(Rect.position);
                    }
                    else
                    {
                        IsVisible = false;
                    }
                }
            }
        }

        private void OnWindow(int id)
        {
            OnDrawGUILayout();
        }

        public void Bind(object target, DebugUIPanelBindHandler handler)
        {
            m_bindTarget = target ?? throw new ArgumentNullException(nameof(target));
            m_bindHandler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void BindClear()
        {
            m_bindTarget = null;
            m_bindHandler = null;
        }

        public T GetBindTarget<T>()
        {
            return (T)m_bindTarget;
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected abstract void OnDrawGUILayout();
    }
}
