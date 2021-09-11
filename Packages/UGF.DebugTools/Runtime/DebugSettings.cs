using UGF.CustomSettings.Runtime;

namespace UGF.DebugTools.Runtime
{
    public static class DebugSettings
    {
        public static CustomSettingsPackage<DebugSettingsAsset> Settings { get; } = new CustomSettingsPackage<DebugSettingsAsset>
        (
            "UGF.DebugTools",
            nameof(DebugSettings)
        );
    }
}
