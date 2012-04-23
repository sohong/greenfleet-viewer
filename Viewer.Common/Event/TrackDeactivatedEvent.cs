////////////////////////////////////////////////////////////////////////////////
// TrackDeactivatedEvent.cs
// 2012.04.23, created by sohong
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
using Viewer.Common.Model;

namespace Viewer.Common.Event
{
    /// <summary>
    /// 트랙이 재생을 종료할 때 발생한다.
    /// </summary>
    public class TrackDeactivatedEvent : CompositePresentationEvent<Track>
    {
    }
}
