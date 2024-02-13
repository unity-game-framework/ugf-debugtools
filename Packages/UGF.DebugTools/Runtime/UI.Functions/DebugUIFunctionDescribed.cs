using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public abstract class DebugUIFunctionDescribed<TDescription> : DebugUIFunction where TDescription : class
    {
        public TDescription Description { get; }

        protected DebugUIFunctionDescribed(TDescription description, GUIContent content) : base(content)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }
    }
}
