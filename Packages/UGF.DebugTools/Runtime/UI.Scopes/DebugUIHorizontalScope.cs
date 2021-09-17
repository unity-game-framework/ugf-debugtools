using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public struct DebugUIHorizontalScope : IDisposable
    {
        public DebugUIHorizontalScope(GUIContent content, params GUILayoutOption[] options) : this(content, GUIStyle.none, options)
        {
        }

        public DebugUIHorizontalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(content, style, options);
        }

        public void Dispose()
        {
            GUILayout.EndHorizontal();
        }
    }
}
