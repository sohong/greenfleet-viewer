////////////////////////////////////////////////////////////////////////////////
// TrackChangedEvent.cs
// 2012.03.21, created by sohong
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

namespace Viewer.Common.Event {

    /// <summary>
    /// 트랙 상태가 변경되었을 때 TrackGroup에서 발생 시킨다.
    /// </summary>
    public class TrackChangedEvent : CompositePresentationEvent<Track> {
    }
}
