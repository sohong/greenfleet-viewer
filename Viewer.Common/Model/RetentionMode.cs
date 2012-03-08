////////////////////////////////////////////////////////////////////////////////
// RetentionMode.cs
// 2012.03.07, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Viewer.Common.Model {

    public enum RetentionMode {

        None,   // 자동 삭제하지 않음.
        Auto    // 지정한 기간이 지나면 자동 삭제.
    }
}
