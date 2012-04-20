////////////////////////////////////////////////////////////////////////////////
// DriveUtil.cs
// 2012.03.22, created by sohong
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
using System.IO;

namespace Viewer.Common.Util
{
    /// <summary>
    /// 드라이브 관련 유틸리티들.
    /// </summary>
    public class DriveUtil
    {
        public static DriveInfo[] GetDrives()
        {
            DriveInfo[] list = DriveInfo.GetDrives();
            return list;
        }
    }
}
