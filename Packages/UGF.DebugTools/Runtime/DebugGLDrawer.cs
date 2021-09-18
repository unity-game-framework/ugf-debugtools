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

        public void DrawGL(Camera camera)
        {
            if (camera == null) throw new ArgumentNullException(nameof(camera));

            for (int i = 0; i < m_commands.Count; i++)
            {
                m_commandsUpdate.Add(m_commands[i]);
            }

            DrawGLCommands(camera, m_commandsUpdate);

            m_commandsUpdate.Clear();

            // lineMaterial.SetPass(0);
            //
            // GL.PushMatrix();
            // // Set transformation matrix for drawing to
            // // match our transform
            // GL.MultMatrix(transform.localToWorldMatrix);
            //
            // // Draw lines
            // GL.Begin(GL.LINES);
            // for (int i = 0; i < lineCount; ++i)
            // {
            //     float a = i / (float)lineCount;
            //     float angle = a * Mathf.PI * 2;
            //     // Vertex colors change from red to green
            //     GL.Color(new Color(a, 1 - a, 0, 0.8F));
            //     // One vertex at transform position
            //     GL.Vertex3(0, 0, 0);
            //     // Another vertex at edge of circle
            //     GL.Vertex3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0);
            // }
            // GL.End();
            // GL.PopMatrix();
        }

        public void DrawGLCommands(Camera camera, IReadOnlyList<DebugGLDrawCommand> commands)
        {
            if (camera == null) throw new ArgumentNullException(nameof(camera));
            if (commands == null) throw new ArgumentNullException(nameof(commands));

            for (int i = 0; i < commands.Count; i++)
            {
                DebugGLDrawCommand command = commands[i];

                if (m_shapes.TryGetValue(command.Shape, out DebugGLShape shape))
                {
                    shape.GetVertices(m_verticesUpdate);

                    DrawGLVertices(m_material, command.Mode, command.Color, m_verticesUpdate);

                    m_verticesUpdate.Clear();
                }
            }
        }

        public void DrawGLVertices(Material material, DebugGLMode mode, Color color, IReadOnlyList<Vector3> vertices)
        {
            if (material == null) throw new ArgumentNullException(nameof(material));

            material.SetPass(0);

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
