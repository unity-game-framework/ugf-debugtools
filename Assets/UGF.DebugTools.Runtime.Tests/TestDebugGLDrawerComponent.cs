using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugGLDrawerComponent : MonoBehaviour
    {
        private class Shape : DebugGLShape
        {
            public Shape()
            {
                Mode = DebugGLMode.LineStrip;
                Vertices.Add(new Vector3(-1F, -1F, 0F));
                Vertices.Add(new Vector3(-1F, 1F, 0F));
                Vertices.Add(new Vector3(1F, 1F, 0F));
                Vertices.Add(new Vector3(1F, -1F, 0F));
            }
        }

        private void Start()
        {
            DebugGL.Drawer.AddShape("test", new Shape());
        }

        private void Update()
        {
            DebugGL.Drawer.AddCommand(new DebugGLDrawCommand("test", DebugGLMode.LineStrip, transform.position, transform.rotation, transform.localScale, Color.red));
        }
    }
}
