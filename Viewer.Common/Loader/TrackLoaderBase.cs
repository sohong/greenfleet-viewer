////////////////////////////////////////////////////////////////////////////////
// TrackLoaderBase.cs
// 2012.03.07, created by sohong
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
using Viewer.Common.Model;
using System.IO;

namespace Viewer.Common.Loader {
    
    /// <summary>
    /// ITrackLoader
    /// </summary>
    public class TrackLoaderBase {

        protected Track LoadFromStream(FileStream stream) {
            Track track = new Track();

            return track;
        }
    }
}
