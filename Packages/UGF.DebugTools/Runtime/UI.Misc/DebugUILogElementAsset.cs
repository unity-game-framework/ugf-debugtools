using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Misc
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Log Element", order = 2000)]
    public class DebugUILogElementAsset : DebugUIElementAsset
    {
        protected override DebugUIElement OnBuild()
        {
            return new DebugUILogElement();
        }
    }
}
