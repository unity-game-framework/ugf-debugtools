using UGF.DebugTools.Runtime;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    [CustomEditor(typeof(DebugUIProviderAsset), true)]
    internal class DebugUIProviderAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnable;
        private SerializedProperty m_propertyScale;
        private SerializedProperty m_propertySkin;
        private AssetIdReferenceListDrawer m_listDrawers;
        private ReorderableListSelectionDrawerByPath m_listDrawersSelection;

        private void OnEnable()
        {
            m_propertyEnable = serializedObject.FindProperty("m_enable");
            m_propertyScale = serializedObject.FindProperty("m_scale");
            m_propertySkin = serializedObject.FindProperty("m_skin");

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
                EditorGUILayout.PropertyField(m_propertyScale);
                EditorGUILayout.PropertyField(m_propertySkin);

                m_listDrawers.DrawGUILayout();
                m_listDrawersSelection.DrawGUILayout();
            }
        }
    }
}
