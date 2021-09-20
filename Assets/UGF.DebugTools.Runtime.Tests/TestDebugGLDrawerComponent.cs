using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugGLDrawerComponent : MonoBehaviour
    {
        private void Update()
        {
            DebugGL.CubeWire(transform.position, transform.rotation, transform.localScale, Color.red);
        }
    }
}
