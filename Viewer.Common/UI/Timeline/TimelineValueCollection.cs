////////////////////////////////////////////////////////////////////////////////
// TimelineValueCollection.cs
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
using Viewer.Common.Model;

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// Collection of TimelineValue.
    /// 각 TimelineValue들이 다른 분(minutes)에서 시작한다고 가정한다.
    /// 분이 연속되지 않는 시점 혹은, value type이 달라질 때 
    /// 새로운 TimelineValue가 생성된다. 
    /// </summary>
    public class TimelineValueCollection : IEnumerable<TimelineValue>
    {
        #region fields

        private IList<TimelineValue> m_values;

        #endregion // fields


        #region constructor

        public TimelineValueCollection(DateTime start, DateTime end)
        {
            StartTime = start.StripSeconds();
            EndTime = end.StripSeconds();

            m_values = new List<TimelineValue>();
        }

        #endregion // constructor


        #region IEnumerable<TimelineValue>

        public IEnumerator<TimelineValue> GetEnumerator()
        {
            return m_values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_values.GetEnumerator();
        }
        
        #endregion // IEnumerable<TimelineValue>


        #region properties

        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public int Count
        {
            get { return m_values.Count; }
        }

        public TimelineValue this[int index]
        {
            get { return m_values[index]; }
        }

        #endregion // properties;


        #region methods

        /// <summary>
        /// 전달되는 트랙들이 시간 순으로 정렬되어 있다고 가정한다!!
        /// </summary>
        public void Build(TrackCollection tracks)
        {
            m_values.Clear();
            if (tracks.Count < 1) return;

            Track track = tracks.First;
            TimelineValue value = new TimelineValue(GetValueType(track), track);
            value.LastTrack = track;
            m_values.Add(value);

            for (int i = 1, count = tracks.Count; i < count; i++) {
                track = tracks[i];
                TimelineValue.TimelineValueType vtype = GetValueType(track);

                // 분이 연속되지 않거나, value type이 달라지면
                double m1 = TimeSpan.FromTicks(track.StartTime.StripSeconds().Ticks).TotalMinutes;
                double m2 = TimeSpan.FromTicks(value.Finish.StripSeconds().Ticks).TotalMinutes + 1;
                if (vtype != value.Type || m1 > m2) {                              
                    value = new TimelineValue(vtype, track);
                    value.LastTrack = track;
                    m_values.Add(value);
                } else {
                    value.LastTrack = track;
                    value.Append(track.StartTime);
                }
            }
        }

        /// <summary>
        /// 0 ~ 1 사이의 x 값에 해당하는 time index를 리턴한다.
        /// </summary>
        public TimelineValue GetValueAt(DateTime t)
        {
            foreach (TimelineValue v in m_values) {
                if (v.Start <= t && t <= v.Finish) {
                    return v;
                }
            }

            return null;
        }

        #endregion // methods


        #region internal methods

        private TimelineValue.TimelineValueType GetValueType(Track track)
        {
            return track.TrackType == TrackType.Event ? TimelineValue.TimelineValueType.Event : TimelineValue.TimelineValueType.All;
        }

        #endregion // internal methods
    }
}
