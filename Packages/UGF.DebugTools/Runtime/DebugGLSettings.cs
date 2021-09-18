using UGF.CustomSettings.Runtime;

namespace UGF.DebugTools.Runtime
{
    public static class DebugGLSettings
    {
        public static CustomSettingsPackage<DebugGLSettingsAsset> Settings { get; } = new CustomSettingsPackage<DebugGLSettingsAsset>
        (
            "UGF.DebugTools",
            nameof(DebugGLSettings)
        );
    }
}
