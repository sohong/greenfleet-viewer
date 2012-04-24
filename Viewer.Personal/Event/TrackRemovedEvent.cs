////////////////////////////////////////////////////////////////////////////////
// TrackRemovedEvent.cs
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
using Viewer.Common.Model;

namespace Viewer.Personal.Event
{
    /// <summary>
    /// Repository에서 트랙이 삭제된 후 발생한다.
    /// </summary>
    public class TrackRemovedEvent : CompositePresentationEvent<Track>
    {
    }
}
