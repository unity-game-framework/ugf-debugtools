using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Function Drawer", order = 2000)]
    public class DebugUIFunctionDrawerAsset : DebugUIDrawerAsset
    {
        protected override IDebugUIDrawer OnBuild()
        {
            return new DebugUIFunctionDrawer();
        }
    }
}
