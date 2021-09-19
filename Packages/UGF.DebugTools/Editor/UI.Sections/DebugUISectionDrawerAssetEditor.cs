using UGF.DebugTools.Runtime.UI.Sections;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor.UI.Sections
{
    [CustomEditor(typeof(DebugUISectionDrawerAsset), true)]
    internal class DebugUISectionDrawerAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyWidth;
        private SerializedProperty m_propertyHeight;
        private SerializedProperty m_propertyAlignment;
        private AssetReferenceListDrawer m_listSections;

        private void OnEnable()
        {
            m_propertyWidth = serializedObject.FindProperty("m_width");
            m_propertyHeight = serializedObject.FindProperty("m_height");
            m_propertyAlignment = serializedObject.FindProperty("m_alignment");
            m_listSections = new AssetReferenceListDrawer(serializedObject.FindProperty("m_sections"));
            m_listSections.Enable();
        }

        private void OnDisable()
        {
            m_listSections.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyWidth);
                EditorGUILayout.PropertyField(m_propertyHeight);
                EditorGUILayout.PropertyField(m_propertyAlignment);

                m_listSections.DrawGUILayout();
            }
        }
    }
}
