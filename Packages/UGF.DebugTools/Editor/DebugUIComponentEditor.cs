using UGF.DebugTools.Runtime;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    [CustomEditor(typeof(DebugUIComponent), true)]
    internal class DebugUIComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnable;
        private SerializedProperty m_propertyScale;
        private SerializedProperty m_propertySkin;
        private AssetReferenceListDrawer m_listDrawers;

        private void OnEnable()
        {
            m_propertyEnable = serializedObject.FindProperty("m_enable");
            m_propertyScale = serializedObject.FindProperty("m_scale");
            m_propertySkin = serializedObject.FindProperty("m_skin");
            m_listDrawers = new AssetReferenceListDrawer(serializedObject.FindProperty("m_drawers"));
            m_listDrawers.Enable();
        }

        private void OnDisable()
        {
            m_listDrawers.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorGUILayout.PropertyField(m_propertyEnable);
                EditorGUILayout.PropertyField(m_propertyScale);
                EditorGUILayout.PropertyField(m_propertySkin);

                m_listDrawers.DrawGUILayout();
            }
        }
    }
}
