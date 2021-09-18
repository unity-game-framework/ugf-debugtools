using UGF.DebugTools.Runtime;
using UGF.EditorTools.Editor.IMGUI.Scopes;
using UnityEditor;

namespace UGF.DebugTools.Editor
{
    [CustomEditor(typeof(DebugGLSettingsAsset), true)]
    internal class DebugGLSettingsAssetEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            using (new SerializedObjectUpdateScope(serializedObject))
            {
            }
        }
    }
}
