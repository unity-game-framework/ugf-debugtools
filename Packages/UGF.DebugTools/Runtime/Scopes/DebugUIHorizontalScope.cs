using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Scopes
{
    public struct DebugUIHorizontalScope : IDisposable
    {
        public DebugUIHorizontalScope(params GUILayoutOption[] options) : this(GUIContent.none, GUIStyle.none, options)
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
