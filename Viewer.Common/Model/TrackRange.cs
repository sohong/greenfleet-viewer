////////////////////////////////////////////////////////////////////////////////
// TrackRange.cs
// 2012.04.02, created by sohong
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

namespace Viewer.Common.Model
{
    /// <summary>
    /// TimelineBar에 표시되는 (같은 tracktype으로)연속되는 track 구간.
    /// </summary>
    public class TrackRange : NotificationObjectEx
    {
        #region constructors

        public TrackRange(TrackType type)
        {
            this.TrackType = type;
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// All(상시) or Event
        /// </summary>
        public TrackType TrackType
        {
            get;
            private set;
        }

        /// <summary>
        /// 구간 시작 track
        /// </summary>
        public Track StartTrack
        {
            get { return m_startTrack; }
            set
            {
                if (value != m_startTrack) {
                    m_startTrack = value;
                    RaisePropertyChanged(() => StartTrack);
                }
            }
        }
        private Track m_startTrack;

        /// <summary>
        /// 구간 종료 track
        /// </summary>
        public Track EndTrack
        {
            get { return m_endTrack; }
            set
            {
                if (value != m_endTrack) {
                    m_endTrack = value;
                    RaisePropertyChanged(() => EndTrack);
                }
            }
        }
        private Track m_endTrack;

        #endregion // properties
    }
}
