using UGF.CustomSettings.Runtime;

namespace UGF.DebugTools.Runtime
{
    public static class DebugUISettings
    {
        public static CustomSettingsPackage<DebugUISettingsAsset> Settings { get; } = new CustomSettingsPackage<DebugUISettingsAsset>
        (
            "UGF.DebugTools",
            nameof(DebugUISettings)
        );
    }
}
