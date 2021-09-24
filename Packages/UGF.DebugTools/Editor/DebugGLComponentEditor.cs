using UGF.DebugTools.Runtime;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    [CustomEditor(typeof(DebugGLComponent), true)]
    internal class DebugGLComponentEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyEnable;
        private SerializedProperty m_propertyDefaultShapes;
        private SerializedProperty m_propertyDefaultMaterial;
        private AssetReferenceListDrawer m_listShapes;

        private void OnEnable()
        {
            m_propertyEnable = serializedObject.FindProperty("m_enable");
            m_propertyDefaultShapes = serializedObject.FindProperty("m_defaultShapes");
            m_propertyDefaultMaterial = serializedObject.FindProperty("m_defaultMaterial");
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
                EditorGUILayout.PropertyField(m_propertyDefaultShapes);
                EditorGUILayout.PropertyField(m_propertyDefaultMaterial);

                m_listShapes.DrawGUILayout();
            }
        }
    }
}
