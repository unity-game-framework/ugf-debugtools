using UGF.DebugTools.Runtime.UI.Functions;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor.UI.Functions
{
    [CustomEditor(typeof(DebugUIFunctionDrawerAsset), true)]
    internal class DebugUIFunctionDrawerAssetEditor : UnityEditor.Editor
    {
        private SerializedProperty m_propertyDisplay;
        private SerializedProperty m_propertyDisplayMenuDisableEventSystem;
        private SerializedProperty m_propertyWidth;
        private ReorderableListDrawer m_listFunctions;
        private ReorderableListSelectionDrawerByElement m_listFunctionsSelection;

        private void OnEnable()
        {
            m_propertyDisplay = serializedObject.FindProperty("m_display");
            m_propertyDisplayMenuDisableEventSystem = serializedObject.FindProperty("m_displayMenuDisableEventSystem");
            m_propertyWidth = serializedObject.FindProperty("m_width");

            m_listFunctions = new ReorderableListDrawer(serializedObject.FindProperty("m_functions"));

            m_listFunctionsSelection = new ReorderableListSelectionDrawerByElement(m_listFunctions)
            {
                Drawer = { DisplayTitlebar = true }
            };

            m_listFunctions.Enable();
            m_listFunctionsSelection.Enable();
        }

        private void OnDisable()
        {
            m_listFunctions.Disable();
            m_listFunctionsSelection.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                EditorGUILayout.PropertyField(m_propertyDisplay);
                EditorGUILayout.PropertyField(m_propertyDisplayMenuDisableEventSystem);
                EditorGUILayout.PropertyField(m_propertyWidth);

                m_listFunctions.DrawGUILayout();
                m_listFunctionsSelection.DrawGUILayout();
            }
        }
    }
}
