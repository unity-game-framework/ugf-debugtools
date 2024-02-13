using UGF.DebugTools.Runtime.UI.Functions;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestDebugUIFunctionAsset")]
    public class TestDebugUIFunctionAsset : DebugUIFunctionAsset
    {
        protected override DebugUIFunction OnBuild()
        {
            return new TestDebugUIFunction();
        }
    }
}
