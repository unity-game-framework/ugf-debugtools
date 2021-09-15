using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Section Drawer", order = 2000)]
    public class DebugUISectionDrawerAsset : DebugUIDrawerAsset
    {
        protected override IDebugUIDrawer OnBuild()
        {
            return new DebugUISectionDrawer();
        }
    }
}
