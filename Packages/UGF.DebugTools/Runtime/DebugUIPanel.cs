using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIPanel
    {
        public Vector3 Position { get; set; }
        public Vector2 Size { get; set; } = Vector2.one * 100F;
        public Rect Rect { get; private set; }

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
            if (Event.current.type == EventType.Repaint)
            {
                Vector3 screenPoint = Camera.current.WorldToScreenPoint(Position);

                Rect = new Rect(screenPoint.x, Screen.height - screenPoint.y, Size.x, Size.y);
            }

            GUILayout.BeginArea(Rect);

            OnDrawGUILayout();

            GUILayout.EndArea();
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
