using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime
{
    internal static class DebugUIContentCache
    {
        private static readonly GUIContent m_contentLabel = new GUIContent();
        private static readonly GUIContent m_contentContent = new GUIContent();

        public static GUIContent GetLabel(string text)
        {
            return SetText(m_contentLabel, text);
        }

        public static GUIContent GetContent(string text)
        {
            return SetText(m_contentContent, text);
        }

        public static void Reset()
        {
            Reset(m_contentLabel);
            Reset(m_contentContent);
        }

        private static GUIContent SetText(GUIContent content, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                content.text = text;

                return content;
            }

            return GUIContent.none;
        }

        private static void Reset(GUIContent content)
        {
            if (content == null) throw new ArgumentNullException(nameof(content));

            content.text = string.Empty;
            content.image = null;
            content.tooltip = string.Empty;
        }
    }
}
