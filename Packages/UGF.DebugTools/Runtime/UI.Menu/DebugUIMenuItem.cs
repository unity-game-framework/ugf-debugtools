using System;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public abstract class DebugUIMenuItem
    {
        public DebugUIMenuItemHandler Handler { get; }
        public bool Selected { get; private set; }
        public object Value { get { return m_value ?? throw new ArgumentException("Value not specified."); } }
        public bool HasValue { get { return m_value != null; } }

        private object m_value;

        protected DebugUIMenuItem(DebugUIMenuItemHandler handler, object value = null)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));

            m_value = value;
        }

        public void DrawGUILayout()
        {
            OnDrawGUILayout();
        }

        public void Reset()
        {
            Selected = false;
        }

        public T GetValue<T>()
        {
            return (T)Value;
        }

        public void SetValue(object value)
        {
            m_value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public void ClearValue()
        {
            m_value = null;
        }

        protected void Select()
        {
            Selected = true;
            Handler?.Invoke(this);
        }

        protected abstract void OnDrawGUILayout();
    }
}
