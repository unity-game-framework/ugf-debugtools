using UGF.DebugTools.Runtime;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    [CustomEditor(typeof(DebugGLSettingsAsset), true)]
    internal class DebugGLSettingsAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnable;
        private AssetReferenceListDrawer m_listShapes;

        private void OnEnable()
        {
            m_propertyEnable = serializedObject.FindProperty("m_enable");
            m_listShapes = new AssetReferenceListDrawer(serializedObject.FindProperty("m_shapes"));
            m_listShapes.Enable();
        }

        private void OnDisable()
        {
            m_listShapes.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyEnable);

                m_listShapes.DrawGUILayout();
            }
        }
    }
}
