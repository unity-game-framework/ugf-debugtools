using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public struct DebugUIVerticalScope : IDisposable
    {
        public DebugUIVerticalScope(GUIContent content, params GUILayoutOption[] options) : this(content, GUIStyle.none, options)
        {
        }

        public DebugUIVerticalScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginVertical(content, style, options);
        }

        public void Dispose()
        {
            GUILayout.EndVertical();
        }
    }
}
