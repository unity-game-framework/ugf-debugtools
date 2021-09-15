using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUIPanelExtensions
    {
        public static void BindTransform(this DebugUIPanel panel, Transform transform)
        {
            if (panel == null) throw new ArgumentNullException(nameof(panel));
            if (transform == null) throw new ArgumentNullException(nameof(transform));

            panel.Bind(transform, target => ((Transform)target).position);
        }
    }
}
