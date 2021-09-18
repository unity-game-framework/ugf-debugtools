using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UGF.DebugTools.Runtime.GL.Scopes;
using UnityEngine;
using UnityEngine.Rendering;

namespace UGF.DebugTools.Runtime
{
    public class DebugGLDrawer
    {
        public IReadOnlyDictionary<string, DebugGLShape> Shapes { get; }
        public IReadOnlyList<DebugGLDrawCommand> Commands { get; }

        private readonly Dictionary<string, DebugGLShape> m_shapes = new Dictionary<string, DebugGLShape>();
        private readonly List<DebugGLDrawCommand> m_commands = new List<DebugGLDrawCommand>();
        private readonly List<DebugGLDrawCommand> m_commandsUpdate = new List<DebugGLDrawCommand>();
        private readonly List<Vector3> m_verticesUpdate = new List<Vector3>();

        private static readonly int m_srcBlend = Shader.PropertyToID("_SrcBlend");
        private static readonly int m_dstBlend = Shader.PropertyToID("_DstBlend");
        private static readonly int m_cull = Shader.PropertyToID("_Cull");
        private static readonly int m_zWrite = Shader.PropertyToID("_ZWrite");
        private Material m_material;

        public DebugGLDrawer()
        {
            Shapes = new ReadOnlyDictionary<string, DebugGLShape>(m_shapes);
            Commands = new ReadOnlyCollection<DebugGLDrawCommand>(m_commands);

            m_material = OnCreateMaterial();
        }

        public void DrawGL()
        {
            for (int i = 0; i < m_commands.Count; i++)
            {
                m_commandsUpdate.Add(m_commands[i]);
            }

            DrawGLCommands(m_commandsUpdate);

            m_commandsUpdate.Clear();
        }

        public void DrawGLCommands(IReadOnlyList<DebugGLDrawCommand> commands)
        {
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            for (int i = 0; i < commands.Count; i++)
            {
                DebugGLDrawCommand command = commands[i];

                if (m_shapes.TryGetValue(command.Shape, out DebugGLShape shape))
                {
                    shape.GetVertices(m_verticesUpdate);

                    Matrix4x4 matrix = Matrix4x4.TRS(command.Position, command.Rotation, command.Scale);

                    DrawGLVertices(m_verticesUpdate, command.Mode, matrix, command.Color, m_material);

                    m_verticesUpdate.Clear();
                }
            }
        }

        public void DrawGLVertices(IReadOnlyList<Vector3> vertices, DebugGLMode mode, Matrix4x4 matrix, Color color, Material material, int pass = 0)
        {
            if (material == null) throw new ArgumentNullException(nameof(material));

            material.SetPass(pass);

            using (new DebugGLMatrixScope(matrix))
            using (new DebugGLDrawScope(mode))
            {
                UnityEngine.GL.Color(color);

                for (int i = 0; i < vertices.Count; i++)
                {
                    UnityEngine.GL.Vertex(vertices[i]);
                }
            }
        }

        private Material OnCreateMaterial()
        {
            Shader shader = Shader.Find("Hidden/Internal-Colored");
            var material = new Material(shader);

            material.SetInteger(m_srcBlend, (int)BlendMode.SrcAlpha);
            material.SetInteger(m_dstBlend, (int)BlendMode.OneMinusSrcAlpha);
            material.SetInteger(m_cull, (int)CullMode.Off);
            material.SetInteger(m_zWrite, 0);

            return material;
        }
    }
}
