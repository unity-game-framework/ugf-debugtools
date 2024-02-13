using System;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIDrawerDescribedAsset<TDrawer, TDescription> : DebugUIDrawerAsset
        where TDrawer : DebugUIDrawer
        where TDescription : class
    {
        protected override DebugUIDrawer OnBuild()
        {
            TDescription description = OnBuildDescription();

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            return OnBuild(description);
        }

        protected abstract TDescription OnBuildDescription();
        protected abstract TDrawer OnBuild(TDescription description);
    }
}
