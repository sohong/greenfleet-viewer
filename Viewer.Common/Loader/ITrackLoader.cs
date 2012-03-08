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

    public interface ITrackLoader {

        Track Load(object source);
    }
}
