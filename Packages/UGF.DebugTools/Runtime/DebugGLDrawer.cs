using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.GL.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugGLDrawer
    {
        public IReadOnlyDictionary<string, DebugGLShape> Shapes { get; }
        public IReadOnlyList<DebugGLDrawCommand> Commands { get; }

        private readonly Dictionary<string, DebugGLShape> m_shapes = new Dictionary<string, DebugGLShape>();
        private readonly List<DebugGLDrawCommand> m_commands = new List<DebugGLDrawCommand>();

        public DebugGLDrawer()
        {
            Shapes = new ReadOnlyDictionary<string, DebugGLShape>(m_shapes);
            Commands = new ReadOnlyCollection<DebugGLDrawCommand>(m_commands);
        }

        public void AddShape(string name, DebugGLShape shape)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));
            if (shape == null) throw new ArgumentNullException(nameof(shape));

            m_shapes.Add(name, shape);
        }

        public bool RemoveShape(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            return m_shapes.Remove(name);
        }

        public void AddCommand(DebugGLDrawCommand command)
        {
            if (!command.IsValid()) throw new ArgumentException("Value should be valid.", nameof(command));

            m_commands.Add(command);
        }

        public void ClearCommands()
        {
            m_commands.Clear();
        }

        public void DrawGL()
        {
            DrawGLCommands(m_commands);

            m_commands.Clear();
        }

        public void DrawGLCommands(IReadOnlyList<DebugGLDrawCommand> commands)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            for (int i = 0; i < commands.Count; i++)
            {
                DebugGLDrawCommand command = commands[i];

                if (m_shapes.TryGetValue(command.Shape, out DebugGLShape shape))
                {
                    Matrix4x4 matrix = Matrix4x4.TRS(command.Position, command.Rotation, command.Scale);
                    Material material = shape.HasMaterial ? shape.Material : DebugGLUtility.DefaultMaterial;

                    DrawGLVertices(shape.Vertices, shape.Mode, matrix, command.Color, material);
                }
            }
        }

        public void DrawGLVertices(IReadOnlyList<Vector3> vertices, DebugGLMode mode, Matrix4x4 matrix, Color color, Material material, int pass = 0)
        {
            if (material == null) throw new ArgumentNullException(nameof(material));

            using (new DebugGLMatrixScope(matrix))
            using (new DebugGLDrawScope(mode))
            {
                material.SetPass(pass);

                UnityEngine.GL.Color(color);

                for (int i = 0; i < vertices.Count; i++)
                {
                    UnityEngine.GL.Vertex(vertices[i]);
                }
            }
        }
    }
}
