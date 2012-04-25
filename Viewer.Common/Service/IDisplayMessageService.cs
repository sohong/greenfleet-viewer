////////////////////////////////////////////////////////////////////////////////
// IDisplayMessageService.cs
// 2012.04.25, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viewer.Common.Service
{
    /// <summary>
    /// 메시지 대화 상자를 표시한다.
    /// </summary>
    public interface IDisplayMessageService
    {
        bool Confirm(string caption, string message);
    }
}
