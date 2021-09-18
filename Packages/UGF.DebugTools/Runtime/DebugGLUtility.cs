using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugGLUtility
    {
        public static Material DefaultMaterial { get; }

        static DebugGLUtility()
        {
            DefaultMaterial = new Material(Shader.Find("Sprites/Default"));
        }
    }
}
