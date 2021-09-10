using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIPanel
    {
        public bool Enabled { get; set; } = true;
        public Vector3 Position { get; set; }
        public Vector2 Size { get; set; } = Vector2.one * 100F;
        public Rect Rect { get; private set; }
        public bool IsVisible { get; private set; }

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
            if (IsVisible)
            {
                GUILayout.BeginArea(Rect);

                OnDrawGUILayout();

                GUILayout.EndArea();
            }

            if (Event.current.type == EventType.Repaint)
            {
                Vector3 screenPoint = Camera.current.WorldToScreenPoint(Position);

                if (screenPoint.z > 0F)
                {
                    var rect = new Rect(screenPoint.x, Screen.height - screenPoint.y, Size.x, Size.y);
                    var screen = new Rect(0F, 0F, Screen.width, Screen.height);

                    Rect = rect;
                    IsVisible = screen.Contains(screenPoint);
                }
                else
                {
                    IsVisible = false;
                }
            }
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
