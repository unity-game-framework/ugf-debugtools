using System;
using System.ComponentModel;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public readonly struct DebugGLDrawCommand
    {
        public string Shape { get; }
        public DebugGLMode Mode { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }
        public Color Color { get; }

        public DebugGLDrawCommand(string shape, DebugGLMode mode, Vector3 position, Quaternion rotation, Vector3 scale, Color color)
        {
            if (string.IsNullOrEmpty(shape)) throw new ArgumentException("Value cannot be null or empty.", nameof(shape));

            Shape = shape;
            Mode = mode;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Color = color;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Shape);
        }

        public bool Equals(DebugGLDrawCommand other)
        {
            return Shape == other.Shape && Mode == other.Mode && Position.Equals(other.Position) && Rotation.Equals(other.Rotation) && Scale.Equals(other.Scale) && Color.Equals(other.Color);
        }

        public override bool Equals(object obj)
        {
            return obj is DebugGLDrawCommand other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Shape != null ? Shape.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int)Mode;
                hashCode = (hashCode * 397) ^ Position.GetHashCode();
                hashCode = (hashCode * 397) ^ Rotation.GetHashCode();
                hashCode = (hashCode * 397) ^ Scale.GetHashCode();
                hashCode = (hashCode * 397) ^ Color.GetHashCode();
                return hashCode;
            }
        }
    }
}
