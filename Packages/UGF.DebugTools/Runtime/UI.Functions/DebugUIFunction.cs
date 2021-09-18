using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public abstract class DebugUIFunction
    {
        public GUIContent Content { get; }

        protected DebugUIFunction(GUIContent content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public void Execute()
        {
            OnExecute();
        }

        public bool Validate()
        {
            return OnValidate();
        }

        protected abstract void OnExecute();
        protected abstract bool OnValidate();
    }
}
