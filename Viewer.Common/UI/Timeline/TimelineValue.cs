﻿////////////////////////////////////////////////////////////////////////////////
// TimelineValue.cs
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
using Viewer.Common;
using Viewer.Common.Model;

namespace Viewer.Common.UI.Timeline
{
    /// <summary>
    /// A range for TimelineBar.
    /// </summary>
    public class TimelineValue
    {
        /// <summary>
        /// Value type.
        /// </summary>
        public enum TimelineValueType
        {
            All,
            Event
        }


        #region constructor

        public TimelineValue(TimelineValueType type, Track track)
        {
            this.Type = type;
            this.Track = track;
            this.Start = track.StartTime.StripSeconds();
            this.Finish = track.StartTime.StripSeconds();
        }

        #endregion // constructor


        #region properties

        public Track Track
        {
            get;
            set;
        }

        public Track LastTrack
        {
            get;
            set;
        }

        public TimelineValueType Type
        {
            get;
            set;
        }

        public DateTime Start
        {
            get;
            set;
        }

        public DateTime Finish
        {
            get;
            set;
        }

        public uint Length
        {
            get { return (uint)new TimeSpan(Finish.Ticks - Start.Ticks).TotalMinutes + 1; }
        }

        #endregion // properties


        #region methods

        public bool Contains(DateTime t)
        {
            return Start <= t && t <= Finish;
        }

        public void Append(DateTime t)
        {
            t = t.StripSeconds();
            if (t < Start) {
                Start = t;
            } else if (t > Finish) {
                Finish = t;
            }
        }

        #endregion // methods
    }
}
