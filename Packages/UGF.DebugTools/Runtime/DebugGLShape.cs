using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugGLShape
    {
        public DebugGLMode Mode { get; set; } = DebugGLMode.Line;
        public List<Vector3> Vertices { get; } = new List<Vector3>();
        public Material Material { get { return m_material ? m_material : throw new ArgumentException("Value not specified."); } }
        public bool HasMaterial { get { return m_material != null; } }

        private Material m_material;

        protected DebugGLShape(Material material = null)
        {
            m_material = material;
        }

        public void PreDraw()
        {
            OnPreDraw();
        }

        public void PostDraw()
        {
            OnPostDraw();
        }

        public void SetMaterial(Material material)
        {
            m_material = material ? material : throw new ArgumentNullException(nameof(material));
        }

        public void ClearMaterial()
        {
            m_material = null;
        }

        protected virtual void OnPreDraw()
        {
        }

        protected virtual void OnPostDraw()
        {
        }
    }
}
