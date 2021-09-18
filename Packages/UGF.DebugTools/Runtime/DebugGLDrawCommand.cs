using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public readonly struct DebugGLDrawCommand
    {
        public string Shape { get; }
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Vector3 Scale { get; }
        public Color Color { get; }
        public DebugGLMode Mode { get; }
        public bool Wireframe { get; }
        public float Width { get; }

        public DebugGLDrawCommand(string shape, Vector3 position, Quaternion rotation, Vector3 scale, Color color, DebugGLMode mode, bool wireframe, float width)
        {
            if (string.IsNullOrEmpty(shape)) throw new ArgumentException("Value cannot be null or empty.", nameof(shape));

            Shape = shape;
            Position = position;
            Rotation = rotation;
            Scale = scale;
            Color = color;
            Mode = mode;
            Wireframe = wireframe;
            Width = width;
        }
    }
}
