using System;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUISection
    {
        public string DisplayName { get; }

        protected DebugUISection(string displayName)
        {
            if (string.IsNullOrEmpty(displayName)) throw new ArgumentException("Value cannot be null or empty.", nameof(displayName));

            DisplayName = displayName;
        }

        public void Enable()
        {
            OnEnable();
        }

        public void Disable()
        {
            OnDisable();
        }

        public void Select()
        {
            OnSelect();
        }

        public void Deselect()
        {
            OnDeselect();
        }

        public void DrawGUILayout()
        {
            OnDrawGUILayout();
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnSelect()
        {
        }

        protected virtual void OnDeselect()
        {
        }

        protected abstract void OnDrawGUILayout();
    }
}
