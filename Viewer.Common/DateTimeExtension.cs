////////////////////////////////////////////////////////////////////////////////
// DateTimeExtension.cs
// 2012.03.28, created by sohong
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

namespace Viewer.Common {

    /// <summary>
    /// DateTime 확장.
    /// </summary>
    public static class DateTimeExtension {

        /// <summary>
        /// 초 값을 제거한다.
        /// </summary>
        public static DateTime StripSeconds(this DateTime d) {
            return d - TimeSpan.FromSeconds(d.Second);
        }
    }
}
