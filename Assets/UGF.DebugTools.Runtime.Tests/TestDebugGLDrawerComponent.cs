using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugGLDrawerComponent : MonoBehaviour
    {
        private void Update()
        {
            DebugGL.CubeWire(transform.position, transform.rotation, transform.localScale, Color.red);
            DebugGL.Line(transform.position, transform.position + transform.forward * transform.localScale.x, Color.blue);
            DebugGL.QuadWire(transform.position, transform.rotation, transform.localScale, Color.green);
        }
    }
}
