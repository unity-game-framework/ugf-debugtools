namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    public class DebugUIStatsSectionDescription
    {
        public DebugUIStatsAlignment Alignment { get; }
        public bool Display { get; }
        public bool DisplayBackground { get; }
        public bool DisplayEnabled { get; }
        public bool DisplayFps { get; }

        public DebugUIStatsSectionDescription(DebugUIStatsAlignment alignment, bool display, bool displayBackground, bool displayEnabled, bool displayFps)
        {
            Alignment = alignment;
            Display = display;
            DisplayBackground = displayBackground;
            DisplayEnabled = displayEnabled;
            DisplayFps = displayFps;
        }
    }
}
