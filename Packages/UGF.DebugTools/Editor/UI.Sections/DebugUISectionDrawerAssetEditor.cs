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
        private AssetReferenceListDrawer m_listSections;

        private void OnEnable()
        {
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

                m_listSections.DrawGUILayout();
            }
        }
    }
}
