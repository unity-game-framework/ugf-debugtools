using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UGF.DebugTools.Runtime.UI.Misc
{
    public class DebugUIStatsElement : DebugUIElement
    {
        public TimeSpan UpdateInterval { get; }

        public static string UssClassName { get; } = "ugf-debugtools-stats";

        private readonly Label m_label;

        public DebugUIStatsElement(TimeSpan updateInterval)
        {
            UpdateInterval = updateInterval;

            pickingMode = PickingMode.Ignore;

            m_label = new Label
            {
                pickingMode = PickingMode.Ignore
            };

            Add(m_label);
            AddToClassList(UssClassName);

            schedule.Execute(OnUpdate).Every((long)updateInterval.TotalMilliseconds);
        }

        private void OnUpdate(TimerState timerState)
        {
            m_label.text = (1F / Time.unscaledDeltaTime).ToString("F1");
        }
    }
}
