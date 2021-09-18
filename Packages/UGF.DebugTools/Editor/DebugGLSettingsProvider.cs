using UGF.CustomSettings.Editor;
using UGF.DebugTools.Runtime;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    internal static class DebugGLSettingsProvider
    {
        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<DebugGLSettingsAsset>("Project/Unity Game Framework/Debug GL", DebugGLSettings.Settings, SettingsScope.Project);
        }
    }
}
