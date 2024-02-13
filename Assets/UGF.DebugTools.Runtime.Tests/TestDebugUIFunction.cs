using UGF.DebugTools.Runtime.UI.Functions;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    public class TestDebugUIFunction : DebugUIFunction
    {
        public TestDebugUIFunction() : base(new GUIContent("Test"))
        {
        }

        protected override void OnExecute()
        {
            Debug.LogWarning("Test");
        }
    }
}
