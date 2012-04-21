////////////////////////////////////////////////////////////////////////////////
// TrackActivatedEvent.cs
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

namespace Viewer.Common.Event
{
    /// <summary>
    /// Track tree나 map 에서 마우스 더블클릭 등으로 재생할 Track을 설정할 때 발생한다.
    /// </summary>
    public class TrackActivatedEvent : CompositePresentationEvent<Track>
    {
    }
}
