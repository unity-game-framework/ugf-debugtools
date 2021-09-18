using UGF.Builder.Runtime;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections
{
    public abstract class DebugUISectionAsset : BuilderAsset<DebugUISection>
    {
        [SerializeField] private string m_displayName;

        public string DisplayName { get { return m_displayName; } set { m_displayName = value; } }
    }
}
