using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    [AddComponentMenu("")]
    public class DebugUIExecuter : MonoBehaviour
    {
        public event Action Drawing;

        private void OnGUI()
        {
            Drawing?.Invoke();
        }
    }
}
