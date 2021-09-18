using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public abstract class DebugUIFunction
    {
        public GUIContent Content { get; }
        public bool Enabled { get; set; }

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

        protected virtual bool OnValidate()
        {
            return true;
        }
    }
}
