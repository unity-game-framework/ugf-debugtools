using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugUIDrawer
    {
        public IReadOnlyCollection<IDebugUIDrawer> Drawers { get; }
        public Vector2 Scale { get; set; } = Vector2.one;
        public GUISkin Skin { get { return HasSkin ? m_skin : throw new ArgumentException("Value not specified."); } }
        public bool HasSkin { get { return m_skin != null; } }

        private readonly List<IDebugUIDrawer> m_drawers = new List<IDebugUIDrawer>();
        private GUISkin m_skin;

        public DebugUIDrawer()
        {
            Drawers = new ReadOnlyCollection<IDebugUIDrawer>(m_drawers);
        }

        public void AddDrawer(IDebugUIDrawer drawer)
        {
            if (drawer == null) throw new ArgumentNullException(nameof(drawer));

            m_drawers.Add(drawer);

            drawer.Enable();
        }

        public bool RemoveDrawer(IDebugUIDrawer drawer)
        {
            if (drawer == null) throw new ArgumentNullException(nameof(drawer));

            if (m_drawers.Remove(drawer))
            {
                drawer.Disable();
                return true;
            }

            return false;
        }

        public void Clear()
        {
            for (int i = 0; i < m_drawers.Count; i++)
            {
                IDebugUIDrawer drawer = m_drawers[i];

                drawer.Disable();
            }

            m_drawers.Clear();
        }

        public void DrawGUI()
        {
            Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Scale.x, Scale.y, 1F));
            GUISkin skin = HasSkin ? Skin : GUI.skin;

            using (new DebugUIMatrixScope(matrix))
            using (new DebugUISkinScope(skin))
            {
                for (int i = 0; i < m_drawers.Count; i++)
                {
                    IDebugUIDrawer drawer = m_drawers[i];

                    drawer.DrawGUI();
                }
            }
        }

        public void SetSkin(GUISkin skin)
        {
            m_skin = skin ? skin : throw new ArgumentNullException(nameof(skin));
        }

        public void ClearSkin()
        {
            m_skin = null;
        }

        public T Get<T>() where T : DebugUIDrawerBase
        {
            return (T)Get(typeof(T));
        }

        public IDebugUIDrawer Get(Type type)
        {
            return TryGet(type, out IDebugUIDrawer drawer) ? drawer : throw new ArgumentException($"Drawer not found by the specified type: '{type}'.");
        }

        public bool TryGet<T>(out T drawer) where T : IDebugUIDrawer
        {
            if (TryGet(typeof(T), out IDebugUIDrawer value))
            {
                drawer = (T)value;
                return true;
            }

            drawer = default;
            return false;
        }

        public bool TryGet(Type type, out IDebugUIDrawer drawer)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));

            for (int i = 0; i < m_drawers.Count; i++)
            {
                drawer = m_drawers[i];

                if (type.IsInstanceOfType(drawer))
                {
                    return true;
                }
            }

            drawer = null;
            return false;
        }
    }
}
