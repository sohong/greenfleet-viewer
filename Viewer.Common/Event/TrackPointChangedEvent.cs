////////////////////////////////////////////////////////////////////////////////
// TrackPointChanged.cs
// 2012.04.05, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Events;
using Viewer.Common.Model;

namespace Viewer.Common.Event {

    public class TrackPointChangedEvent : CompositePresentationEvent<TrackPoint> {
    }
}
