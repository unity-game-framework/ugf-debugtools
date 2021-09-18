using UGF.DebugTools.Runtime.UI.Functions;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static partial class DebugUI
    {
        public static DebugUIFunction AddFunction(string groupName, string content, DebugUIFunctionExecuteHandler executeHandler)
        {
            return AddFunction(groupName, new GUIContent(content), executeHandler);
        }

        public static DebugUIFunction AddFunction(string groupName, string content, DebugUIFunctionExecuteHandler executeHandler, DebugUIFunctionValidateHandler validateHandler)
        {
            return AddFunction(groupName, new GUIContent(content), executeHandler, validateHandler);
        }

        public static DebugUIFunction AddFunction(string groupName, GUIContent content, DebugUIFunctionExecuteHandler executeHandler)
        {
            return Drawer.Get<DebugUIFunctionDrawer>().Add(groupName, content, executeHandler);
        }

        public static DebugUIFunction AddFunction(string groupName, GUIContent content, DebugUIFunctionExecuteHandler executeHandler, DebugUIFunctionValidateHandler validateHandler)
        {
            return Drawer.Get<DebugUIFunctionDrawer>().Add(groupName, content, executeHandler, validateHandler);
        }

        public static void AddFunction(string groupName, DebugUIFunction function)
        {
            Drawer.Get<DebugUIFunctionDrawer>().Add(groupName, function);
        }

        public static bool RemoveFunction(string groupName, DebugUIFunction function)
        {
            return Drawer.Get<DebugUIFunctionDrawer>().Remove(groupName, function);
        }
    }
}
