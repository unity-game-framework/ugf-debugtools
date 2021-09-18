using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugGLShape
    {
        public void GetVertices(ICollection<Vector3> vertices)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            OnGetVertices(vertices);
        }

        protected abstract void OnGetVertices(ICollection<Vector3> vertices);
    }
}
