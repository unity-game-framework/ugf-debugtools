using UGF.DebugTools.Runtime.UI.Sections;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
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
        private AssetIdReferenceListDrawer m_listSections;
        private ReorderableListSelectionDrawerByPath m_listSectionsSelection;

        private void OnEnable()
        {
            m_propertyWidth = serializedObject.FindProperty("m_width");
            m_propertyHeight = serializedObject.FindProperty("m_height");
            m_propertyAlignment = serializedObject.FindProperty("m_alignment");

            m_listSections = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_sections"));

            m_listSectionsSelection = new ReorderableListSelectionDrawerByPath(m_listSections, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listSections.Enable();
            m_listSectionsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listSections.Disable();
            m_listSectionsSelection.Disable();
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
                m_listSectionsSelection.DrawGUILayout();
            }
        }
    }
}
