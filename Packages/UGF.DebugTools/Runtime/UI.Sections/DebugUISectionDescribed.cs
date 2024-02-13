using System;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public abstract class DebugUISectionDescribed<TDescription> : DebugUISection where TDescription : class
    {
        public TDescription Description { get; }

        protected DebugUISectionDescribed(TDescription description, string displayName) : base(displayName)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
