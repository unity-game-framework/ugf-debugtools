using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public abstract class DebugUIFunctionAsset : BuilderAsset<DebugUIFunction>
    {
        [SerializeField] private string m_groupName = "Debug";

        public string GroupName { get { return m_groupName; } set { m_groupName = value; } }
    }
}
