using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [AddComponentMenu("")]
    public class DebugUIEventComponent : MonoBehaviour
    {
        public event Action DrawGUI;

        private void OnGUI()
        {
            DrawGUI?.Invoke();
        }
    }
}
