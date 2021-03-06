using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.GL.Scopes;
using UGF.Initialize.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugGLDrawer : InitializeBase
    {
        public bool Enable { get; set; } = true;
        public IReadOnlyDictionary<string, DebugGLShape> Shapes { get; }
        public IReadOnlyList<DebugGLDrawCommand> Commands { get; }
        public Material DefaultMaterial { get { return m_defaultMaterial != null ? m_defaultMaterial : throw new ArgumentException("Value not specified."); } }
        public bool HasDefaultMaterial { get { return m_defaultMaterial != null; } }

        private readonly Dictionary<string, DebugGLShape> m_shapes = new Dictionary<string, DebugGLShape>();
        private readonly List<DebugGLDrawCommand> m_commands = new List<DebugGLDrawCommand>();
        private Material m_defaultMaterial;

        public DebugGLDrawer()
        {
            Shapes = new ReadOnlyDictionary<string, DebugGLShape>(m_shapes);
            Commands = new ReadOnlyCollection<DebugGLDrawCommand>(m_commands);
        }

        public void AddShape(string id, DebugGLShape shape)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));
            if (shape == null) throw new ArgumentNullException(nameof(shape));

            m_shapes.Add(id, shape);
        }

        public bool RemoveShape(string id)
        {
            if (string.IsNullOrEmpty(id)) throw new ArgumentException("Value cannot be null or empty.", nameof(id));

            return m_shapes.Remove(id);
        }

        public void ClearShapes()
        {
            m_shapes.Clear();
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
            if (Enable)
            {
                DrawGLCommands(m_commands);
            }
        }

        public void DrawGLCommands(IReadOnlyList<DebugGLDrawCommand> commands)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            for (int i = 0; i < commands.Count; i++)
            {
                DebugGLDrawCommand command = commands[i];

                if (m_shapes.TryGetValue(command.ShapeId, out DebugGLShape shape))
                {
                    Matrix4x4 matrix = Matrix4x4.TRS(command.Position, command.Rotation, command.Scale);

                    DrawGLVertices(shape.Vertices, shape.Mode, matrix, command.Color, command.Material);
                }
                else
                {
                    throw new ArgumentException($"Shape not found by the specified id: '{command.ShapeId}'.");
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

        public void SetDefaultMaterial(Material material)
        {
            m_defaultMaterial = material ? material : throw new ArgumentNullException(nameof(material));
        }

        public void ClearDefaultMaterial()
        {
            m_defaultMaterial = null;
        }
    }
}
