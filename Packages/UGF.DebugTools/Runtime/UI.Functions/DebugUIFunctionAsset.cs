using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public abstract class DebugUIFunctionAsset : BuilderAsset<DebugUIFunction>
    {
        [SerializeField] private string m_groupName = "Debug";
        [SerializeField] private string m_label = "Function";

        public string GroupName { get { return m_groupName; } set { m_groupName = value; } }
        public string Label { get { return m_label; } set { m_label = value; } }
    }
}
