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

        public static DebugGLShape CreateShapeLineWire()
        {
            return CreateShapeLineWire(DefaultMaterial);
        }

        public static DebugGLShape CreateShapeLineWire(Material material)
        {
            var shape = new DebugGLShape(DebugGLMode.Line, material);

            shape.Vertices.Add(new Vector3(0F, 0F, 0F));
            shape.Vertices.Add(new Vector3(0F, 0F, 1F));

            return shape;
        }

        public static DebugGLShape CreateShapeQuadWire()
        {
            return CreateShapeQuadWire(DefaultMaterial);
        }

        public static DebugGLShape CreateShapeQuadWire(Material material)
        {
            var shape = new DebugGLShape(DebugGLMode.Line, material);

            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, 0F, 0F), Quaternion.Euler(0F, 0F, 0F), Vector3.one));
            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, 0F, 0F), Quaternion.Euler(0F, 90F, 0F), Vector3.one));

            return shape;
        }

        public static DebugGLShape CreateShapeCircleWire()
        {
            return CreateShapeCircleWire(DefaultMaterial);
        }

        public static DebugGLShape CreateShapeCircleWire(Material material)
        {
            var shape = new DebugGLShape(DebugGLMode.Line, material);

            AddVerticesCircleLines(shape.Vertices, Matrix4x4.identity);

            return shape;
        }

        public static DebugGLShape CreateShapeCubeWire()
        {
            return CreateShapeCubeWire(DefaultMaterial);
        }

        public static DebugGLShape CreateShapeCubeWire(Material material)
        {
            var shape = new DebugGLShape(DebugGLMode.Line, material);

            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, 0.5F, 0F), Quaternion.Euler(0F, 0F, 0F), Vector3.one));
            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, 0.5F, 0F), Quaternion.Euler(0F, 90F, 0F), Vector3.one));
            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, -0.5F, 0F), Quaternion.Euler(0F, 0F, 0F), Vector3.one));
            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, -0.5F, 0F), Quaternion.Euler(0F, 90F, 0F), Vector3.one));
            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, 0F, 0.5F), Quaternion.Euler(90F, 0F, 0F), Vector3.one));
            AddVerticesQuad(shape.Vertices, Matrix4x4.TRS(new Vector3(0F, 0F, -0.5F), Quaternion.Euler(90F, 0F, 0F), Vector3.one));

            return shape;
        }

        public static void AddVerticesCircleLines(ICollection<Vector3> vertices, Matrix4x4 matrix, float size = 0.5F, int segments = 16, float degrees = 360F)
        {
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));

            float angle = degrees / segments;

            for (float i = 0; i < degrees; i += angle)
            {
                Vector3 start = Quaternion.Euler(0F, i, 0F) * Vector3.forward * size;
                Vector3 end = Quaternion.Euler(0F, i + angle, 0F) * Vector3.forward * size;

                start = matrix.MultiplyPoint3x4(start);
                end = matrix.MultiplyPoint3x4(end);

                vertices.Add(start);
                vertices.Add(end);
            }
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
