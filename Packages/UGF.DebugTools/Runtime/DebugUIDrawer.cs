using UGF.Initialize.Runtime;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIDrawer : InitializeBase
    {
        public void DrawGUI()
        {
            OnDrawGUI();
        }

        protected abstract void OnDrawGUI();
    }
}
