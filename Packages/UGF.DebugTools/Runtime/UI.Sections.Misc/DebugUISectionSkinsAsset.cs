using System.Collections.Generic;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    [CreateAssetMenu(menuName = "Unity Game Framework/Debug/Debug UI Skin Section", order = 2000)]
    public class DebugUISectionSkinsAsset : DebugUISectionAsset
    {
        [SerializeField] private List<GUISkin> m_skins = new List<GUISkin>();

        public List<GUISkin> Skins { get { return m_skins; } }

        protected override DebugUISection OnBuild()
        {
            var drawer = new DebugUISectionSkins();

            drawer.Skins.AddRange(m_skins);

            return drawer;
        }
    }
}
