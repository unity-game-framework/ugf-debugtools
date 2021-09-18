using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    public abstract class DebugUIPanel : DebugUIWindowDrawer
    {
        public Rect Position { get; private set; } = new Rect(0F, 0F, 200F, 200F);
        public bool IsVisible { get; private set; }
        public object BindTarget { get { return m_bindTarget ?? throw new ArgumentException("Value not specified."); } }
        public bool HasBindTarget { get { return m_bindTarget != null; } }
        public DebugUIPanelBindHandler BindHandler { get { return m_bindHandler ?? throw new ArgumentException("Value not specified."); } }
        public bool HasBindHandler { get { return m_bindHandler != null; } }

        private object m_bindTarget;
        private DebugUIPanelBindHandler m_bindHandler;

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

        protected override Rect OnGetPosition()
        {
            return Position;
        }

        protected override void OnDrawGUI()
        {
            if (IsVisible || !HasBindTarget)
            {
                base.OnDrawGUI();
            }

            if (Display && Event.current.type == EventType.Repaint)
            {
                if (HasBindTarget)
                {
                    Vector3 targetPosition = m_bindHandler.Invoke(m_bindTarget);

                    if (DebugUIUtility.TryWorldToGUIPosition(targetPosition, out Vector2 position))
                    {
                        Rect screen = DebugUIUtility.GetScreenRect();

                        Position = new Rect(position, Position.size);
                        IsVisible = screen.Contains(position);
                    }
                    else
                    {
                        IsVisible = false;
                    }
                }
                else
                {
                    IsVisible = true;
                }
            }
        }
    }
}
