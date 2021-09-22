using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugUIDrawer
    {
        public bool Enable { get; set; } = true;
        public IReadOnlyDictionary<string, IDebugUIDrawer> Drawers { get; }
        public Vector2 Scale { get; set; } = Vector2.one;
        public GUISkin Skin { get { return HasSkin ? m_skin : throw new ArgumentException("Value not specified."); } }
        public bool HasSkin { get { return m_skin != null; } }

        private readonly Dictionary<string, IDebugUIDrawer> m_drawers = new Dictionary<string, IDebugUIDrawer>();
        private readonly Dictionary<string, IDebugUIDrawer> m_drawersUpdate = new Dictionary<string, IDebugUIDrawer>();
        private GUISkin m_skin;

        public DebugUIDrawer()
        {
            Drawers = new ReadOnlyDictionary<string, IDebugUIDrawer>(m_drawers);
        }

        public void Add(string id, IDebugUIDrawer drawer)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (drawer == null) throw new ArgumentNullException(nameof(drawer));

            m_drawers.Add(id, drawer);

            drawer.Enable();
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            if (m_drawers.TryGetValue(id, out IDebugUIDrawer drawer))
            {
                m_drawers.Remove(id);
                drawer.Disable();
                return true;
            }

            return false;
        }

        public void Clear()
        {
            foreach (KeyValuePair<string, IDebugUIDrawer> pair in m_drawers)
            {
                pair.Value.Disable();
            }

            m_drawers.Clear();
        }

        public void DrawGUI()
        {
            if (Enable)
            {
                foreach (KeyValuePair<string, IDebugUIDrawer> pair in m_drawers)
                {
                    m_drawersUpdate.Add(pair.Key, pair.Value);
                }

                Matrix4x4 matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(Scale.x, Scale.y, 1F));
                GUISkin skin = HasSkin ? Skin : GUI.skin;

                using (new DebugUIMatrixScope(matrix))
                using (new DebugUISkinScope(skin))
                {
                    foreach (KeyValuePair<string, IDebugUIDrawer> pair in m_drawersUpdate)
                    {
                        pair.Value.DrawGUI();
                    }
                }

                m_drawersUpdate.Clear();
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

            foreach (KeyValuePair<string, IDebugUIDrawer> pair in m_drawers)
            {
                if (type.IsInstanceOfType(pair.Value))
                {
                    drawer = pair.Value;
                    return true;
                }
            }

            drawer = null;
            return false;
        }

        public T Get<T>(string id)
        {
            return (T)Get(id);
        }

        public IDebugUIDrawer Get(string id)
        {
            return TryGet(id, out IDebugUIDrawer drawer) ? drawer : throw new ArgumentException($"Drawer not found by the specified id: '{id}'.");
        }

        public bool TryGet<T>(string id, out T drawer) where T : IDebugUIDrawer
        {
            if (TryGet(id, out IDebugUIDrawer value))
            {
                drawer = (T)value;
                return true;
            }

            drawer = default;
            return false;
        }

        public bool TryGet(string id, out IDebugUIDrawer drawer)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_drawers.TryGetValue(id, out drawer);
        }
    }
}
