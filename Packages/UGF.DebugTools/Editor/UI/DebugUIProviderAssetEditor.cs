using UGF.DebugTools.Runtime.UI;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor.UI
{
    [CustomEditor(typeof(DebugUIProviderAsset), true)]
    internal class DebugUIProviderAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnable;
        private SerializedProperty m_propertyDocumentGameObjectName;
        private SerializedProperty m_propertyPanelSettings;
        private AssetIdReferenceListDrawer m_listDrawers;
        private ReorderableListSelectionDrawerByPath m_listDrawersSelection;

        private void OnEnable()
        {
            m_propertyEnable = serializedObject.FindProperty("m_enable");
            m_propertyDocumentGameObjectName = serializedObject.FindProperty("m_documentGameObjectName");
            m_propertyPanelSettings = serializedObject.FindProperty("m_panelSettings");

            m_listDrawers = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_drawers"));

            m_listDrawersSelection = new ReorderableListSelectionDrawerByPath(m_listDrawers, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listDrawers.Enable();
            m_listDrawersSelection.Enable();
        }

        private void OnDisable()
        {
            m_listDrawers.Disable();
            m_listDrawersSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyEnable);
                EditorGUILayout.PropertyField(m_propertyDocumentGameObjectName);
                EditorGUILayout.PropertyField(m_propertyPanelSettings);

                m_listDrawers.DrawGUILayout();
                m_listDrawersSelection.DrawGUILayout();
            }
        }
    }
}
