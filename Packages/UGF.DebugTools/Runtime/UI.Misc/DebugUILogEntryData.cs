using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Misc
{
    public readonly struct DebugUILogEntryData
    {
        public DateTime Time { get; }
        public LogType Type { get; }
        public string Message { get; }
        public string Body { get; }

        public DebugUILogEntryData(DateTime time, LogType type, string message, string body)
        {
            if (string.IsNullOrEmpty(message)) throw new ArgumentException("Value cannot be null or empty.", nameof(message));
            if (string.IsNullOrEmpty(body)) throw new ArgumentException("Value cannot be null or empty.", nameof(body));

            Time = time;
            Type = type;
            Message = message;
            Body = body;
        }
    }
}
