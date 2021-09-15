﻿using System;
using UGF.DebugTools.Runtime.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIPanel
    {
        public bool Enabled { get; set; } = true;
        public Vector3 Position { get; set; }
        public Vector2 Size { get; set; } = Vector2.one * 250F;
        public Vector2 Scale { get; set; } = Vector2.one;
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
            if (Enabled)
            {
                if (IsVisible)
                {
                    Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Scale.x, Scale.y, 1F));

                    using (new DebugUIMatrixScope(matrix))
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

                    Vector3 screenPoint = Camera.current.WorldToScreenPoint(Position);

                    if (screenPoint.z > 0F)
                    {
                        var rect = new Rect(screenPoint.x / Scale.x, (Screen.height - screenPoint.y) / Scale.y, Size.x, Size.y);
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