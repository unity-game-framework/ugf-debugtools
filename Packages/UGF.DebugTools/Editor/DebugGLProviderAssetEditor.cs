using UGF.DebugTools.Runtime;
using UGF.EditorTools.Editor.Assets;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    [CustomEditor(typeof(DebugGLProviderAsset), true)]
    internal class DebugGLProviderAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnable;
        private SerializedProperty m_propertyDefaultShapes;
        private SerializedProperty m_propertyDefaultMaterial;
        private AssetIdReferenceListDrawer m_listShapes;
        private ReorderableListSelectionDrawerByPath m_listShapesSelection;

        private void OnEnable()
        {
            m_propertyEnable = serializedObject.FindProperty("m_enable");
            m_propertyDefaultShapes = serializedObject.FindProperty("m_defaultShapes");
            m_propertyDefaultMaterial = serializedObject.FindProperty("m_defaultMaterial");

            m_listShapes = new AssetIdReferenceListDrawer(serializedObject.FindProperty("m_shapes"));

            m_listShapesSelection = new ReorderableListSelectionDrawerByPath(m_listShapes, "m_asset")
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listShapes.Enable();
            m_listShapesSelection.Enable();
        }

        private void OnDisable()
        {
            m_listShapes.Disable();
            m_listShapesSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyEnable);
                EditorGUILayout.PropertyField(m_propertyDefaultShapes);
                EditorGUILayout.PropertyField(m_propertyDefaultMaterial);

                m_listShapes.DrawGUILayout();
                m_listShapesSelection.DrawGUILayout();
            }
        }
    }
}
