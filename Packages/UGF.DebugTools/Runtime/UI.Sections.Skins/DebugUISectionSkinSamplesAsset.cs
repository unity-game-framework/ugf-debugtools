using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Skins
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Skin Samples Section", order = 2000)]
    public class DebugUISectionSkinSamplesAsset : DebugUISectionAsset
    {
        protected override DebugUISection OnBuild()
        {
            return new DebugUISectionSkinSamples();
        }
    }
}
