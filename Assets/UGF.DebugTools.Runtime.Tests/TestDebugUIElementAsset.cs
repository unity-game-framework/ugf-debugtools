using UGF.DebugTools.Runtime.UI;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.Tests
{
    [CreateAssetMenu(menuName = "Tests/TestDebugUIElementAsset")]
    public class TestDebugUIElementAsset : DebugUIElementAsset
    {
        [SerializeField] private string m_message;

        public string Message { get { return m_message; } set { m_message = value; } }

        protected override DebugUIElement OnBuild()
        {
            return new TestDebugUIElement(m_message);
        }
    }

    public class TestDebugUIElement : DebugUIElement
    {
        public TestDebugUIElement(string message)
        {
            Add(new Label(message));
            Add(new Button(OnMessageLogs) { text = "Test Message Logs" });
        }

        private void OnMessageLogs()
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log($"Test Message '{i}'");
            }
        }
    }
}
