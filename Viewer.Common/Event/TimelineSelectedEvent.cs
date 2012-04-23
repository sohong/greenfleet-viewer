////////////////////////////////////////////////////////////////////////////////
// TimelineSelectedEvent.cs
// 2012.04.24, created by sohong
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
using Microsoft.Practices.Prism.Events;
using Viewer.Common.UI.Timeline;

namespace Viewer.Common.Event
{
    public class TimelineEventArg
    {
        public TimelineEventArg(TimelineValue line, DateTime time)
        {
            this.Timeline = line;
            this.Time = time;
        }

        public TimelineValue Timeline;
        public DateTime Time;
    }

    /// <summary>
    /// TimelineBar에서 트래커를 드래깅하여 지점을 선택했을 때 발생한다.
    /// </summary>
    public class TimelineSelectedEvent : CompositePresentationEvent<TimelineEventArg>
    {
    }
}
