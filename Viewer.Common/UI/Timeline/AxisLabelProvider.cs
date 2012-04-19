////////////////////////////////////////////////////////////////////////////////
// AxisLabelProvider.cs
// 2012.04.19, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viewer.Common.UI.Timeline {

    /// <summary>
    /// TimelineBar X축에 표시할 label들의 위치와 text를 리턴한다.
    /// </summary>
    public class AxisLabelProvider {

        #region fields

        private IList<double> m_hours;
        private IList<DateTime> m_times;
        
        #endregion // fields


        #region constructor

        public AxisLabelProvider() {
            m_hours = new List<double>();
            m_times = new List<DateTime>();

            m_hours.Add(0);
            m_hours.Add(1);

            m_times.Add(DateTime.MinValue);
            m_times.Add(DateTime.MinValue);
        }

        #endregion // constructor

        
        #region properties

        public DateTime StartTime {
            get;
            set;
        }

        public DateTime EndTime {
            get;
            set;
        }

        public int Count {
            get { return m_hours.Count; }
        }

        #endregion // properties


        #region methods

        public void RefreshLabels() {
            m_hours.Clear();
            m_times.Clear();

            DateTime t = StartTime;
            t = new DateTime(t.Year, t.Month, t.Day, t.Hour, 0, 0);
            double x = 0;
            m_hours.Add(x);
            m_times.Add(t);

            t = t.AddHours(1);
            while (t < EndTime) {
                x = GetPosition(t);
                m_hours.Add(x);
                m_times.Add(t);

                t = t.AddHours(1);
            }

            x = 1;
            t = EndTime;
            m_hours.Add(x);
            m_times.Add(t);
        }

        public double GetPosition(int index) {
            return m_hours[index];
        }

        public DateTime GetTime(int index) {
            return m_times[index];
        }

        public string GetLabel(int index) {
            return m_times[index].ToShortTimeString();
        }

        public double GetPosition(DateTime t) {
            double p = (double)(t.Ticks - StartTime.Ticks + 1) / (EndTime.Ticks - StartTime.Ticks + 1);
            return p;
        }

        #endregion // methods
    }
}
