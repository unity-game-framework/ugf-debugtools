using System;
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
    }
}
