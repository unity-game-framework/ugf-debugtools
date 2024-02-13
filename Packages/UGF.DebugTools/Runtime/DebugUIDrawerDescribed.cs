using System;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIDrawerDescribed<TDescription> : DebugUIDrawer where TDescription : class
    {
        public TDescription Description { get; }

        protected DebugUIDrawerDescribed(TDescription description)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
