using System;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public abstract class DebugUIFunctionDescribedAsset<TFunction, TDescription> : DebugUIFunctionAsset
        where TFunction : DebugUIFunction
        where TDescription : class
    {
        protected override DebugUIFunction OnBuild()
        {
            TDescription description = OnBuildDescription();

            if (description == null)
            {
                throw new ArgumentNullException(nameof(description));
            }

            return OnBuild(description);
        }

        protected abstract TDescription OnBuildDescription();
        protected abstract TFunction OnBuild(TDescription description);
    }
}
