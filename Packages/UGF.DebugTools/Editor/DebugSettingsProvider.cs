using UGF.CustomSettings.Editor;
using UGF.DebugTools.Runtime;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    internal static class DebugSettingsProvider
    {
        [SettingsProvider]
        private static SettingsProvider GetProvider()
        {
            return new CustomSettingsProvider<DebugSettingsAsset>("Project/Unity Game Framework/Debug", DebugSettings.Settings, SettingsScope.Project);
        }
    }
}
