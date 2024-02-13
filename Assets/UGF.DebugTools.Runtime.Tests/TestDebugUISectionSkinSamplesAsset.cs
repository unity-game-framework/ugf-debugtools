using UGF.DebugTools.Runtime.UI.Sections;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestDebugUISectionSkinSamplesAsset")]
    public class TestDebugUISectionSkinSamplesAsset : DebugUISectionAsset
    {
        protected override DebugUISection OnBuild()
        {
            return new TestDebugUISectionSkinSamples();
        }
    }
}
