using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public struct DebugUICenterScope : IDisposable
    {
        public DebugUICenterScope(GUIContent content, params GUILayoutOption[] options) : this(content, GUIStyle.none, options)
        {
        }

        public DebugUICenterScope(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
        {
            GUILayout.BeginHorizontal(content, style, options);
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical(content, style, options);
            GUILayout.FlexibleSpace();
        }

        public void Dispose()
        {
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
    }
}
