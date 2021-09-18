using System;
using UGF.DebugTools.Runtime.UI.Sections.Skins;
using UGF.EditorTools.Editor.IMGUI;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor.UI.Sections.Skins
{
    [CustomEditor(typeof(DebugUISectionSkinsAsset), true)]
    internal class DebugUISectionSkinsAssetEditor : UnityEditor.Editor
    {
        private ReorderableListDrawer m_listSkins;

        private void OnEnable()
        {
            m_listSkins = new ReorderableListDrawer(serializedObject.FindProperty("m_skins"));
            m_listSkins.Enable();
        }

        private void OnDisable()
        {
            m_listSkins.Disable();
        }

        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
                EditorIMGUIUtility.DrawScriptProperty(serializedObject);

                m_listSkins.DrawGUILayout();
            }
        }
    }
}
