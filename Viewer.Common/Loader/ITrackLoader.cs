////////////////////////////////////////////////////////////////////////////////
// ITrackLoader.cs
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
using Viewer.Common.Model;

namespace Viewer.Common.Loader {

    /// <summary>
    /// 임의의 소스에서 트랙정보를 읽어들인다.
    /// </summary>
    public interface ITrackLoader {

        Track Load(object source, bool convertVideo);
    }
}
