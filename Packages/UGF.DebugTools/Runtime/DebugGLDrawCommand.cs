using System;
using System.ComponentModel;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public readonly struct DebugGLDrawCommand
    {
        public string ShapeId { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }
        public Color Color { get; }

        public DebugGLDrawCommand(string shapeId, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            if (string.IsNullOrEmpty(shapeId)) throw new ArgumentException("Value cannot be null or empty.", nameof(shapeId));

            ShapeId = shapeId;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Color = color;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(ShapeId);
        }

        public bool Equals(DebugGLDrawCommand other)
        {
            return ShapeId == other.ShapeId && Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale) && Color.Equals(other.Color);
        }

        public override bool Equals(object obj)
        {
            return obj is DebugGLDrawCommand other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = ShapeId != null ? ShapeId.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Rotation.GetHashCode();
                hashCode = (hashCode * 397) ^ Scale.GetHashCode();
                hashCode = (hashCode * 397) ^ Color.GetHashCode();
                return hashCode;
            }
        }
    }
}
