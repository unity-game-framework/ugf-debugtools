using System;
using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugGLUtility
    {
        public static Material DefaultMaterial { get; }

        static DebugGLUtility()
        {
            DefaultMaterial = new Material(Shader.Find("Hidden/Internal-Colored"));
        }

        public static void AddVerticesCircle(ICollection<Vector3> vertices, int segments, float degrees = 360F)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            float angle = degrees / segments;

            for (float i = 0F; i < degrees; i += angle)
            {
                Vector3 vertex = Quaternion.Euler(0F, 0F, i) * Vector3.up * 0.5F;

                vertices.Add(vertex);
            }

            vertices.Add(Quaternion.Euler(0F, 0F, 0F) * Vector3.up * 0.5F);
        }
    }
}
