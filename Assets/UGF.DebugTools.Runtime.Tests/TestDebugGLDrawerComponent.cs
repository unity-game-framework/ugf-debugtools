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
            DebugGL.CircleWire(transform.position + transform.up, transform.rotation, transform.localScale, Color.cyan);
            DebugGL.SphereWire(transform.position + transform.right, transform.rotation, transform.localScale, Color.yellow);
            DebugGL.CylinderWire(transform.position - transform.right, transform.rotation, transform.localScale, Color.yellow);
        }
    }
}
