using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugGL
    {
        public static string ShapeCubeWireId { get; } = Guid.NewGuid().ToString("N");

        public static void CubeWire(Transform transform, Color color)
        {
            CubeWire(transform.position, transform.rotation, transform.localScale, color);
        }

        public static void CubeWire(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Shape(ShapeCubeWireId, position, rotation, scale, color);
        }

        public static void Shape(string id, Transform transform, Color color)
        {
            if (transform == null) throw new ArgumentNullException(nameof(transform));

            Shape(id, transform.position, transform.rotation, transform.localScale, color);
        }

        public static void Shape(string id, Vector3 position, Color color)
        {
            Shape(id, position, Quaternion.identity, Vector3.one, color);
        }

        public static void Shape(string id, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            Drawer.AddCommand(new DebugGLDrawCommand(id, position, rotation, scale, color));
        }

        public static DebugGLShape CreateCubeWire(Material material, float size = 0.5F)
        {
            if (material == null) throw new ArgumentNullException(nameof(material));

            var shape = new DebugGLShape(DebugGLMode.LineStrip, material);

            DebugGLUtility.AddVerticesCube(shape.Vertices, Matrix4x4.identity, size);

            return shape;
        }
    }
}
