using UGF.Initialize.Runtime;

namespace UGF.DebugTools.Runtime
{
    public abstract class DebugUIDrawerBase : InitializeBase, IDebugUIDrawer
    {
        public void DrawGUI()
        {
            OnDrawGUI();
        }

        protected abstract void OnDrawGUI();
    }
}
