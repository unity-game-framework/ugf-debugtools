﻿using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Scopes
{
    public readonly struct DebugUIGroupScope : IDisposable
    {
        public DebugUIGroupScope(Rect position) : this(position, GUIContent.none, GUIStyle.none)
        {
        }

        public DebugUIGroupScope(Rect position, GUIContent content, GUIStyle style)
        {
            GUI.BeginGroup(position, content, style);
        }

        public void Dispose()
        {
            GUI.EndGroup();
        }
    }
}
