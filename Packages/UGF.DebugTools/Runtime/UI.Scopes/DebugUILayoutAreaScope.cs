﻿using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public readonly struct DebugUILayoutAreaScope : IDisposable
    {
        public DebugUILayoutAreaScope(Rect position) : this(position, GUIContent.none, GUIStyle.none)
        {
        }

        public DebugUILayoutAreaScope(Rect position, GUIContent content, GUIStyle style)
        {
            GUILayout.BeginArea(position, content, style);
        }

        public void Dispose()
        {
            GUILayout.EndArea();
        }
    }
}
