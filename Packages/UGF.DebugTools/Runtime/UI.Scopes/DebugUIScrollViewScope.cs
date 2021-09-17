using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public struct DebugUIScrollViewScope : IDisposable
    {
        public Vector2 ScrollPosition { get; private set; }

        public DebugUIScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal = false, bool alwaysShowVertical = false, params GUILayoutOption[] options)
        {
            ScrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, options);
        }

        public DebugUIScrollViewScope(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
        {
            ScrollPosition = GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background, options);
        }

        public void Dispose()
        {
            GUILayout.EndScrollView();
        }
    }
}
