using System;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public abstract class DebugUIMenuItem
    {
        public DebugUIMenuItemHandler Handler { get; }
        public bool Selected { get; private set; }

        protected DebugUIMenuItem(DebugUIMenuItemHandler handler)
        {
            Handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void DrawGUILayout()
        {
            OnDrawGUILayout();
        }

        public void Reset()
        {
            Selected = false;
        }

        protected void Select()
        {
            Selected = true;
            Handler?.Invoke(this);
        }

        protected abstract void OnDrawGUILayout();
    }
}
