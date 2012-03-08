////////////////////////////////////////////////////////////////////////////////
// LocalTrackLoader.cs
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
using Viewer.Loader;
using Viewer.Model;
using System.IO;

namespace Viewer.Personal {

    /// <summary>
    /// Local file을 읽어 Track을 생성한다.
    /// </summary>
    public class LocalTrackLoader : TrackLoaderBase, ITrackLoader {

        #region ITrackLoader

        public Track Load(object source) {
            FileStream stream = new FileStream((string)source, FileMode.Open);
            Track track = LoadFromStream(stream);
            return track;
        }

        #endregion // ITrackLoader
    }
}
