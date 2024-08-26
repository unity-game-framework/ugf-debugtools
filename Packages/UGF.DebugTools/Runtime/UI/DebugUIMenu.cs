using System;

namespace UGF.DebugTools.Runtime.UI
{
    public readonly struct DebugUIMenu
    {
        public string Name { get; }
        public DebugUIElement Element { get; }

        public DebugUIMenu(string name, DebugUIElement element)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Value cannot be null or empty.", nameof(name));

            Name = name;
            Element = element ?? throw new ArgumentNullException(nameof(element));
        }
    }
}
