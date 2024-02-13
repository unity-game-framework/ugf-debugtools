using System.Collections.Generic;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenu
    {
        public List<DebugUIMenuItem> Items { get; } = new List<DebugUIMenuItem>();

        public void Reset()
        {
            for (int i = 0; i < Items.Count; i++)
            {
                Items[i].Reset();
            }
        }
    }
}
