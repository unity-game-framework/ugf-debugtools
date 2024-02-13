using System;
using System.Collections.Generic;
using UGF.DebugTools.Runtime.GL.Scopes;
using UGF.EditorTools.Runtime.Ids;
using UGF.Initialize.Runtime;
using UGF.RuntimeTools.Runtime.Providers;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public class DebugGLProvider : InitializeBase
    {
        public Provider<GlobalId, DebugGLShape> Shapes { get; } = new Provider<GlobalId, DebugGLShape>();
        public List<DebugGLDrawCommand> Commands { get; } = new List<DebugGLDrawCommand>();
        public bool Enable { get; set; } = true;
        public Material DefaultMaterial { get { return m_defaultMaterial != null ? m_defaultMaterial : throw new ArgumentException("Value not specified."); } }
        public bool HasDefaultMaterial { get { return m_defaultMaterial != null; } }

        private Material m_defaultMaterial;

        public void DrawGL()
        {
            if (Enable)
            {
                DrawGLCommands(Commands);
            }
        }

        public void DrawGLCommands(IReadOnlyList<DebugGLDrawCommand> commands)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            for (int i = 0; i < commands.Count; i++)
            {
                DebugGLDrawCommand command = commands[i];

                if (Shapes.TryGet(command.ShapeId, out DebugGLShape shape))
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
            if (vertices == null) throw new ArgumentNullException(nameof(vertices));
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
