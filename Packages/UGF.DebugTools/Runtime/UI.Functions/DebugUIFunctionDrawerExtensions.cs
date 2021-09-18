using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public static class DebugUIFunctionDrawerExtensions
    {
        public static DebugUIFunction Add(this DebugUIFunctionDrawer drawer, string groupName, GUIContent content, DebugUIFunctionExecuteHandler executeHandler)
        {
            if (drawer == null) throw new ArgumentNullException(nameof(drawer));

            var function = new DebugUIFunctionHandler(content, executeHandler);

            drawer.Add(groupName, function);

            return function;
        }

        public static DebugUIFunction Add(this DebugUIFunctionDrawer drawer, string groupName, GUIContent content, DebugUIFunctionExecuteHandler executeHandler, DebugUIFunctionValidateHandler validateHandler)
        {
            if (drawer == null) throw new ArgumentNullException(nameof(drawer));

            var function = new DebugUIFunctionHandler(content, executeHandler, validateHandler);

            drawer.Add(groupName, function);

            return function;
        }
    }
}
