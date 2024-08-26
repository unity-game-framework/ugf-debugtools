using System;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI.Misc
{
    public class DebugUILogEntryElement : VisualElement
    {
        public static string UssClassName { get; } = "ugf-debugtools-log__entry";
        public static string BodyUssClassName { get; } = "ugf-debugtools-log__entry__body";

        private readonly Foldout m_foldout;
        private readonly Button m_buttonCopy;
        private readonly Label m_body;
        private DebugUILogEntryData m_data;

        public DebugUILogEntryElement()
        {
            m_foldout = new Foldout
            {
                toggleOnLabelClick = true
            };

            m_buttonCopy = new Button
            {
                text = "Copy"
            };

            m_body = new Label();

            m_foldout.contentContainer.Add(m_buttonCopy);
            m_foldout.contentContainer.Add(m_body);

            Add(m_foldout);
            AddToClassList(UssClassName);

            m_body.AddToClassList(BodyUssClassName);

            m_buttonCopy.clicked += OnCopyClicked;
        }

        public void Bind(DebugUILogEntryData data)
        {
            m_foldout.value = false;
            m_foldout.text = OnGetHeader(data);
            m_body.text = data.Body;
            m_data = data;
        }

        public void Unbind()
        {
            m_foldout.value = false;
            m_foldout.text = string.Empty;
            m_body.text = string.Empty;
            m_data = default;
        }

        private void OnCopyClicked()
        {
            var builder = new StringBuilder();

            builder.Append(m_data.Type);
            builder.Append(' ');
            builder.AppendLine(m_data.Message);
            builder.Append(m_data.Body);

            GUIUtility.systemCopyBuffer = builder.ToString();
        }

        private string OnGetHeader(DebugUILogEntryData data)
        {
            var builder = new StringBuilder();

            Color color = OnGetTypeColor(data.Type);

            builder.Append('[');
            builder.Append(data.Time.ToString("hh:mm:ss"));
            builder.Append("] <color=#");
            builder.Append(ColorUtility.ToHtmlStringRGB(color));
            builder.Append("><b>");
            builder.Append(data.Type.ToString().ToUpper());
            builder.Append("</b></color> ");
            builder.Append(data.Message);

            return builder.ToString();
        }

        private Color OnGetTypeColor(LogType type)
        {
            return type switch
            {
                LogType.Log => Color.white,
                LogType.Warning => Color.yellow,
                LogType.Error or LogType.Assert or LogType.Exception => Color.red,
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Debug UI Log type is unknown.")
            };
        }
    }
}
