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

        public static void AddVerticesCube(ICollection<Vector3> vertices, Matrix4x4 matrix, float size = 0.5F)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            AddVerticesQuad(vertices, matrix * Matrix4x4.TRS(Vector3.up * size, Quaternion.identity, Vector3.one), size);
            AddVerticesQuad(vertices, matrix * Matrix4x4.TRS(Vector3.down * size, Quaternion.identity, Vector3.one), size);
            AddVerticesQuad(vertices, matrix * Matrix4x4.TRS(Vector3.forward * size, Quaternion.identity, Vector3.one), size);
            AddVerticesQuad(vertices, matrix * Matrix4x4.TRS(Vector3.back * size, Quaternion.identity, Vector3.one), size);
        }

        public static void AddVerticesCircle(ICollection<Vector3> vertices, Matrix4x4 matrix, float size = 0.5F, int segments = 16, float degrees = 360F)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            float angle = degrees / segments;

            for (float i = 0F; i < degrees; i += angle)
            {
                Vector3 vertex = Quaternion.Euler(0F, i, 0F) * Vector3.forward * size;

                vertex = matrix.MultiplyPoint3x4(vertex);

                vertices.Add(vertex);
            }
        }

        public static void AddVerticesQuad(ICollection<Vector3> vertices, Matrix4x4 matrix, float size = 0.5F)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            vertices.Add(matrix.MultiplyPoint3x4(new Vector3(-size, 0F, -size)));
            vertices.Add(matrix.MultiplyPoint3x4(new Vector3(-size, 0F, size)));
            vertices.Add(matrix.MultiplyPoint3x4(new Vector3(size, 0F, size)));
            vertices.Add(matrix.MultiplyPoint3x4(new Vector3(size, 0F, -size)));
        }
    }
}
