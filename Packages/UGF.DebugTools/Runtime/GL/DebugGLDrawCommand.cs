using System;
using UGF.EditorTools.Runtime.Ids;
using UnityEngine;

namespace UGF.DebugTools.Runtime.GL
{
    public readonly struct DebugGLDrawCommand : IEquatable<DebugGLDrawCommand>
    {
        public GlobalId ShapeId { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }
        public Color Color { get; }
        public Material Material { get; }

        public DebugGLDrawCommand(GlobalId shapeId, Vector3 position, Quaternion rotation, Vector3 scale, Color color, Material material)
        {
            if (!shapeId.IsValid()) throw new ArgumentException("Value should be valid.", nameof(shapeId));

            ShapeId = shapeId;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Color = color;
            Material = material ? material : throw new ArgumentNullException(nameof(material));
        }

        public bool IsValid()
        {
            return ShapeId.IsValid() && Material != null;
        }

        public bool Equals(DebugGLDrawCommand other)
        {
            return ShapeId.Equals(other.ShapeId) && Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale) && Color.Equals(other.Color) && Equals(Material, other.Material);
        }

        public override bool Equals(object obj)
        {
            return obj is DebugGLDrawCommand other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ShapeId, Position, Rotation, Scale, Color, Material);
        }
    }
}
