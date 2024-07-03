using System;
using UnityEngine;

namespace UGF.DebugTools.Runtime.UI.Sections.Misc
{
    public struct DebugUISectionLogData
    {
        public bool Foldout { get; set; }
        public string Header { get; set; }
        public DateTime Time { get; set; }
        public LogType Type { get; set; }
        public string Message { get; set; }
        public string Body { get; set; }
    }
}
