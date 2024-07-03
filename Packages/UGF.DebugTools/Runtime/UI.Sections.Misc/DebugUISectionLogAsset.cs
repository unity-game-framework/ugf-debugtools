using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Log Section", order = 2000)]
    public class DebugUISectionLogAsset : DebugUISectionAsset
    {
        protected override DebugUISection OnBuild()
        {
            return new DebugUISectionLog();
        }
    }
}
