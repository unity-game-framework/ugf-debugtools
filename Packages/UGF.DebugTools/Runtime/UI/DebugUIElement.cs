using UGF.Initialize.Runtime;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI
{
    public abstract class DebugUIElement : InitializeBase
    {
        public VisualElement CreateElement()
        {
            return OnCreateElement();
        }

        protected abstract VisualElement OnCreateElement();
    }
}
