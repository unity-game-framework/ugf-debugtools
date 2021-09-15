using UGF.CustomSettings.Editor;
using UGF.DebugTools.Runtime;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    internal static class DebugUISettingsProvider
    {
        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<DebugUISettingsAsset>("Project/Unity Game Framework/Debug UI", DebugUISettings.Settings, SettingsScope.Project);
        }
    }
}
