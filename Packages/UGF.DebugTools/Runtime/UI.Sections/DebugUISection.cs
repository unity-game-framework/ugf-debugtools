using System;
using UGF.Initialize.Runtime;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public abstract class DebugUISection : InitializeBase
    {
        public string DisplayName { get; }

        protected DebugUISection(string displayName)
        {
            if (string.IsNullOrEmpty(displayName)) throw new ArgumentException("Value cannot be null or empty.", nameof(displayName));

            DisplayName = displayName;
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

        protected virtual void OnSelect()
        {
        }

        protected virtual void OnDeselect()
        {
        }

        protected abstract void OnDrawGUILayout();
    }
}
