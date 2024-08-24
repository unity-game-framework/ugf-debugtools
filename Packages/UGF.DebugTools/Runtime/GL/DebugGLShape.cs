using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime.GL
{
    public class DebugGLShape
    {
        public DebugGLMode Mode { get; }
        public List<Vector3> Vertices { get; } = new List<Vector3>();

        public DebugGLShape(DebugGLMode mode)
        {
            Mode = mode;
        }
    }
}
