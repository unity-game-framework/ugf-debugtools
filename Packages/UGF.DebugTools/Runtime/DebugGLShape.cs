using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugGLShape
    {
        public DebugGLMode Mode { get; }
        public Material Material { get; }
        public List<Vector3> Vertices { get; } = new List<Vector3>();

        protected DebugGLShape(DebugGLMode mode, Material material = null)
        {
            Mode = mode;
            Material = material ? material : DebugGLUtility.DefaultMaterial;
        }
    }
}
