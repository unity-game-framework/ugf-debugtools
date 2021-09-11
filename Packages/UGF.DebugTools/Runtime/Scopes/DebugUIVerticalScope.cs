using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.Scopes
{
    public struct DebugUIVerticalScope : IDisposable
    {
        public DebugUIVerticalScope(params GUILayoutOption[] options) : this(GUIContent.none, GUIStyle.none, options)
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
