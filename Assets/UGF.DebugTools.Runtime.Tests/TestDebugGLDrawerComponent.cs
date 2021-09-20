using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugGLDrawerComponent : MonoBehaviour
    {
        private void Update()
        {
            DebugGL.CubeWire(transform, Color.blue);
        }
    }
}
