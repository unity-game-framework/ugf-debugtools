using UGF.EditorTools.Runtime.IMGUI.Attributes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugGLDrawerComponent : MonoBehaviour
    {
        [AssetGuid(typeof(DebugGLShapeAsset))]
        [SerializeField] private string m_shape;

        private void Update()
        {
            DebugGL.Shape(m_shape, transform, Color.red);
        }
    }
}
