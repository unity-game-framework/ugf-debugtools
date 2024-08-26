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
        private SerializedProperty m_propertyDocumentGameObjectName;
        private SerializedProperty m_propertyPanelSettings;
        private AssetIdReferenceListDrawer m_listElements;
        private ReorderableListSelectionDrawerByPath m_listElementsSelection;

        private void OnEnable()
        {
            m_propertyDocumentGameObjectName = serializedObject.FindProperty("m_documentGameObjectName");
            m_propertyPanelSettings = serializedObject.FindProperty("m_panelSettings");

            m_listElements = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_elements"));

            m_listElementsSelection = new ReorderableListSelectionDrawerByPath(m_listElements, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listElements.Enable();
            m_listElementsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listElements.Disable();
            m_listElementsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyDocumentGameObjectName);
                EditorGUILayout.PropertyField(m_propertyPanelSettings);

                m_listElements.DrawGUILayout();
                m_listElementsSelection.DrawGUILayout();
            }
        }
    }
}
