using UGF.DebugTools.Runtime.UI.Sections;

namespace UGF.DebugTools.Runtime.GL
{
    public class DebugGLOptionsSection : DebugUISection
    {
        public DebugGLOptionsSection() : base("GL Options")
        {
        }

        protected override void OnDrawGUILayout()
        {
            DebugGL.Provider.Enable = DebugUI.FieldToggle("Enable", DebugGL.Provider.Enable);
        }
    }
}
