using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Panels
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Panel Drawer", order = 2000)]
    public class DebugUIPanelDrawerAsset : DebugUIDrawerAsset
    {
        protected override IDebugUIDrawer OnBuild()
        {
            return new DebugUIPanelDrawer();
        }
    }
}
