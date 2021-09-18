using UGF.DebugTools.Runtime;
using UGF.EditorTools.Editor.IMGUI.AssetReferences;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    [CustomEditor(typeof(DebugGLSettingsAsset), true)]
    internal class DebugGLSettingsAssetEditor : UnityEditor.Editor
    {
        private AssetReferenceListDrawer m_listShapes;

        private void OnEnable()
        {
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
                m_listShapes.DrawGUILayout();
            }
        }
    }
}
