using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugGLShape
    {
        public DebugGLMode Mode { get; }
        public Material Material { get; }
        public List<Vector3> Vertices { get; } = new List<Vector3>();

        public DebugGLShape(DebugGLMode mode) : this(mode, DebugGLUtility.DefaultMaterial)
        {
        }

        public DebugGLShape(DebugGLMode mode, Material material)
        {
            Mode = mode;
            Material = material ? material : throw new ArgumentNullException(nameof(material));
        }
    }
}
