using UGF.DebugTools.Runtime.UI;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor.UI
{
    [CustomEditor(typeof(DebugUIMenuElementAsset), true)]
    internal class DebugUIMenuElementAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyDisplayName;
        private ReorderableListKeyAndValueDrawer m_listMenu;
        private ReorderableListSelectionDrawerByPath m_listMenuSelection;

        private void OnEnable()
        {
            m_propertyDisplayName = serializedObject.FindProperty("m_displayName");
            m_listMenu = new ReorderableListKeyAndValueDrawer(serializedObject.FindProperty("m_menu"), "m_name", "m_element");

            m_listMenuSelection = new ReorderableListSelectionDrawerByPath(m_listMenu, "m_element")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listMenu.Enable();
            m_listMenuSelection.Enable();
        }

        private void OnDisable()
        {
            m_listMenu.Disable();
            m_listMenuSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyDisplayName);

                m_listMenu.DrawGUILayout();
                m_listMenuSelection.DrawGUILayout();
            }
        }
    }
}
