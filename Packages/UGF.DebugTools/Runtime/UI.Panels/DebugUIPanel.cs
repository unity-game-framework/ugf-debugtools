using System;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public abstract class DebugUIPanel
    {
        public bool Draw { get; set; } = true;
        public Vector3 Position { get; set; }
        public Vector2 Size { get; set; } = Vector2.one * 250F;
        public Rect Rect { get; private set; }
        public bool IsVisible { get; private set; }
        public object BindTarget { get { return m_bindTarget ?? throw new ArgumentException("Value not specified."); } }
        public bool HasBindTarget { get { return m_bindTarget != null; } }
        public DebugUIPanelBindHandler BindHandler { get { return m_bindHandler ?? throw new ArgumentException("Value not specified."); } }
        public bool HasBindHandler { get { return m_bindHandler != null; } }

        private object m_bindTarget;
        private DebugUIPanelBindHandler m_bindHandler;

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
            if (Draw)
            {
                if (IsVisible)
                {
                    using (new DebugUILayoutAreaScope(Rect))
                    {
                        OnDrawGUILayout();
                    }
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
