using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Options Section", order = 2000)]
    public class DebugUISectionOptionsAsset : DebugUISectionAsset
    {
        protected override DebugUISection OnBuild()
        {
            return new DebugUISectionOptions();
        }
    }
}
