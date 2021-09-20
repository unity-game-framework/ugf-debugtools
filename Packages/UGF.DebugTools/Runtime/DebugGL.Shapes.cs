using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugGL
    {
        public static string ShapeLineWireId { get; } = Guid.NewGuid().ToString("N");
        public static string ShapeQuadWireId { get; } = Guid.NewGuid().ToString("N");
        public static string ShapeCircleWireId { get; } = Guid.NewGuid().ToString("N");
        public static string ShapeCubeWireId { get; } = Guid.NewGuid().ToString("N");
        public static string ShapeSphereWireId { get; } = Guid.NewGuid().ToString("N");
        public static string ShapeCylinderWireId { get; } = Guid.NewGuid().ToString("N");

        public static void Line(Vector3 start, Vector3 end, Color color)
        {
            Vector3 direction = end - start;
            Quaternion rotation = direction != Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.identity;
            Vector3 scale = Vector3.one * direction.magnitude;

            Shape(ShapeLineWireId, start, rotation, scale, color);
        }

        public static void QuadWire(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Shape(ShapeQuadWireId, position, rotation, scale, color);
        }

        public static void CircleWire(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Shape(ShapeCircleWireId, position, rotation, scale, color);
        }

        public static void CubeWire(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Shape(ShapeCubeWireId, position, rotation, scale, color);
        }

        public static void SphereWire(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Shape(ShapeSphereWireId, position, rotation, scale, color);
        }

        public static void CylinderWire(Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            Shape(ShapeCylinderWireId, position, rotation, scale, color);
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
    }
}
