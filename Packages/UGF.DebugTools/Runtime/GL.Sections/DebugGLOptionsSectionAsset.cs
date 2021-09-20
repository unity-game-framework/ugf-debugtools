using UGF.DebugTools.Runtime.UI.Sections;
using UnityEngine;

namespace UGF.DebugTools.Runtime.GL.Sections
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug GL Options Section", order = 2000)]
    public class DebugGLOptionsSectionAsset : DebugUISectionAsset
    {
        protected override DebugUISection OnBuild()
        {
            return new DebugGLOptionsSection();
        }
    }
}
