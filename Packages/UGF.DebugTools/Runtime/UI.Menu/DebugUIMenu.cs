using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace UGF.DebugTools.Runtime.UI.Menu
{
    public class DebugUIMenu
    {
        public IReadOnlyList<DebugUIMenuItem> Items { get; }

        private readonly List<DebugUIMenuItem> m_items = new List<DebugUIMenuItem>();

        public DebugUIMenu()
        {
            Items = new ReadOnlyCollection<DebugUIMenuItem>(m_items);
        }

        public void Add(DebugUIMenuItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            m_items.Add(item);
        }

        public bool Remove(DebugUIMenuItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            return m_items.Remove(item);
        }

        public void Clear()
        {
            m_items.Clear();
        }

        public void Reset()
        {
            for (int i = 0; i < m_items.Count; i++)
            {
                m_items[i].Reset();
            }
        }
    }
}
