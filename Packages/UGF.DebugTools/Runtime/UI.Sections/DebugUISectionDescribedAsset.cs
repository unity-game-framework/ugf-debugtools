using System;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public abstract class DebugUISectionDescribedAsset<TSection, TDescription> : DebugUISectionAsset
        where TSection : DebugUISection
        where TDescription : class
    {
        protected override DebugUISection OnBuild()
        {
            TDescription description = OnBuildDescription();

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            return OnBuild(description);
        }

        protected abstract TDescription OnBuildDescription();
        protected abstract TSection OnBuild(TDescription description);
    }
}
