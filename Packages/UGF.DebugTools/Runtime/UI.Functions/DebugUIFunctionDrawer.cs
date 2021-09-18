using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Functions
{
    public class DebugUIFunctionDrawer : DebugUIWindowDrawer
    {
        public bool DisplayMenu { get; set; }

        private readonly GUILayoutOption[] m_buttonDebugOptions = { GUILayout.Width(DebugUI.LineHeight) };

        public DebugUIFunctionDrawer()
        {
            DisplayBackground = false;
        }

        protected override void OnUpdatePosition()
        {
            base.OnUpdatePosition();

            Rect screen = DebugUIUtility.GetScreenRect();

            screen.height = DebugUI.LineHeight;

            Position = screen;
        }

        protected override void OnDrawGUILayout()
        {
            if (DisplayMenu && Event.current.type == EventType.Repaint)
            {
                GUI.skin.window.Draw(Position, false, false, false, false);
            }

            using (new DebugUIHorizontalScope(GUIContent.none))
            {
                if (GUILayout.Button("D", m_buttonDebugOptions))
                {
                    DisplayMenu = !DisplayMenu;
                }

                if (DisplayMenu)
                {
                }
            }
        }
    }
}
