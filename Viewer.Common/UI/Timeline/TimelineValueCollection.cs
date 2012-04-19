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

namespace Viewer.Common.UI.Timeline {

    /// <summary>
    /// Collection of TimelineValue.
    /// 각 TimelineValue들이 다른 분(minutes)에서 시작한다고 가정한다.
    /// 분이 연속되지 않는 시점 혹은, value type이 달라질 때 
    /// 새로운 TimelineValue가 생성된다. 
    /// </summary>
    public class TimelineValueCollection {

        #region fields

        private IList<TimelineValue> m_values;
        private TimelineValue m_current;
        
        #endregion // fields


        #region constructor

        public TimelineValueCollection(DateTime start, DateTime end) {
            StartTime = start.StripSeconds();
            EndTime = end.StripSeconds();

            m_values = new List<TimelineValue>();
        }

        #endregion // constructor


        #region properties

        public DateTime StartTime {
            get;
            private set;
        }

        public DateTime EndTime {
            get;
            private set;
        }

        #endregion // properties;


        #region methods

        public void Build(TrackCollection tracks) {
            m_values.Clear();
            if (tracks.Count < 1) return;

            Track track = tracks.First;
            TimelineValue value = new TimelineValue(GetValueType(track), track.StartTime);
            m_values.Add(value);

            for (int i = 1, count = tracks.Count; i < count; i++) {
                track = tracks[i];
                TimelineValue.TimelineValueType vtype = GetValueType(track);

                if (track.StartTime.Minute > value.Finish.Minute + 1 || // 분이 연속되지 않거나
                    vtype != value.Type) {                              // value type이 달라지면
                    
                    value = new TimelineValue(vtype, track.StartTime);
                    m_values.Add(value);
                } else {
                    value.Append(track.StartTime);
                }
            }
        }

        #endregion // methods


        #region internal methods

        private TimelineValue.TimelineValueType GetValueType(Track track) {
            return track.TrackType == TrackType.Event ? TimelineValue.TimelineValueType.Event : TimelineValue.TimelineValueType.All;
        }

        #endregion // internal methods
    }
}
