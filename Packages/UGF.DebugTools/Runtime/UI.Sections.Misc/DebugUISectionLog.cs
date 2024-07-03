using System;
using System.Collections.Generic;
using System.Text;
using UGF.DebugTools.Runtime.UI.Scopes;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    public class DebugUISectionLog : DebugUISection
    {
        private readonly List<DebugUISectionLogData> m_log = new List<DebugUISectionLogData>();
        private readonly StringBuilder m_builder = new StringBuilder();
        private Styles m_styles;
        private Vector2 m_scroll;

        private class Styles
        {
            public GUIStyle Frame { get; } = new GUIStyle(DebugUIStyles.Frame);
        }

        public DebugUISectionLog() : base("UI Log")
        {
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Application.logMessageReceivedThreaded += OnLogMessageReceived;
        }

        protected override void OnUninitialize()
        {
            base.OnUninitialize();

            Application.logMessageReceivedThreaded -= OnLogMessageReceived;
        }

        protected override void OnDrawGUILayout()
        {
            m_styles ??= new Styles();

            OnDrawToolbar();

            using var scrollViewScope = new DebugUIScrollViewScope(m_scroll, false, false, GUILayout.ExpandHeight(true));

            for (int i = 0; i < m_log.Count; i++)
            {
                DebugUISectionLogData log = m_log[i];

                OnDrawLog(ref log);

                m_log[i] = log;
            }

            m_scroll = scrollViewScope.ScrollPosition;
        }

        private void OnDrawToolbar()
        {
            using (new DebugUIHorizontalScope(GUIContent.none))
            {
                if (GUILayout.Button("Clear"))
                {
                    OnClear();
                }
            }
        }

        private void OnDrawLog(ref DebugUISectionLogData log)
        {
            using (new DebugUIVerticalScope(GUIContent.none, m_styles.Frame))
            {
                using (new DebugUIHorizontalScope(GUIContent.none))
                {
                    log.Foldout = DebugUI.Foldout(log.Header, log.Foldout);
                }

                if (log.Foldout)
                {
                    if (GUILayout.Button("Copy"))
                    {
                        OnCopy(log);
                    }

                    GUILayout.Label(log.Body);
                }
            }
        }

        private void OnClear()
        {
            m_log.Clear();
        }

        private void OnCopy(DebugUISectionLogData log)
        {
            m_builder.Append(log.Type);
            m_builder.Append(' ');
            m_builder.AppendLine(log.Message);
            m_builder.Append(log.Body);

            GUIUtility.systemCopyBuffer = m_builder.ToString();

            m_builder.Clear();
        }

        private void OnLogMessageReceived(string condition, string stacktrace, LogType type)
        {
            Color color = OnLogTypeColor(type);

            m_builder.Append('[');
            m_builder.Append(DateTime.Now.ToString("hh:mm:ss"));
            m_builder.Append("] <color=#");
            m_builder.Append(ColorUtility.ToHtmlStringRGB(color));
            m_builder.Append("><b>");
            m_builder.Append(type.ToString().ToUpper());
            m_builder.Append("</b></color> ");
            m_builder.Append(condition);

            m_log.Add(new DebugUISectionLogData
            {
                Foldout = false,
                Header = m_builder.ToString(),
                Body = stacktrace,
                Type = type,
                Message = condition
            });

            m_builder.Clear();
        }

        private Color OnLogTypeColor(LogType type)
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
