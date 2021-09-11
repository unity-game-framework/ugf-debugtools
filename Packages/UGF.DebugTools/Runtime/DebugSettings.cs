using UGF.CustomSettings.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    public static class DebugSettings
    {
        public static CustomSettingsPackage<DebugSettingsAsset> Settings { get; } = new CustomSettingsPackage<DebugSettingsAsset>
        (
            "UGF.DebugTools",
            nameof(DebugSettings)
        );

        public static GUISkin GetDefaultSkin()
        {
            DebugSettingsAsset data = Settings.GetData();

            return data.DefaultSkin;
        }
    }
}
