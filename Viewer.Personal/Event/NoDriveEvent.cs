////////////////////////////////////////////////////////////////////////////////
// NoDriveEvent.cs
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

namespace Viewer.Personal.Event
{
    /// <summary>
    /// 시스템 드라이브들을 확인 후 greenfleet track data 드라이브가 존재하지 않으면 발생.
    /// </summary>
    public class NoDriveEvent : CompositePresentationEvent<object>
    {
    }
}
