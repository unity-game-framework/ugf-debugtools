using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public class DebugUIFunctionHandler : DebugUIFunction
    {
        public DebugUIFunctionExecuteHandler ExecuteHandler { get; }
        public DebugUIFunctionValidateHandler ValidateHandler { get; }

        public DebugUIFunctionHandler(GUIContent content, DebugUIFunctionExecuteHandler executeHandler) : this(content, executeHandler, function => true)
        {
        }

        public DebugUIFunctionHandler(GUIContent content, DebugUIFunctionExecuteHandler executeHandler, DebugUIFunctionValidateHandler validateHandler) : base(content)
        {
            ExecuteHandler = executeHandler ?? throw new ArgumentNullException(nameof(executeHandler));
            ValidateHandler = validateHandler ?? throw new ArgumentNullException(nameof(validateHandler));
        }

        protected override void OnExecute()
        {
            ExecuteHandler.Invoke(this);
        }

        protected override bool OnValidate()
        {
            return ValidateHandler.Invoke(this);
        }
    }
}
