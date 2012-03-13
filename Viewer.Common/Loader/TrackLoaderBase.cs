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
using System.Text.RegularExpressions;

namespace Viewer.Common.Loader {
    
    /// <summary>
    /// ITrackLoader
    /// </summary>
    public class TrackLoaderBase {

        protected void LoadPoints(Track track, TextReader reader) {
            if (track == null)
                throw new ArgumentNullException("track");
            if (reader == null)
                throw new ArgumentNullException("reader");

            string line;
            while ((line = reader.ReadLine()) != null) {
                if (Regex.IsMatch(line, @"^\d{4}-\d{2}-\d{2}")) {
                    string[] items = line.Split2(',');
                    if (items.Length >= 7) {
                        TrackPoint p = new TrackPoint();

                        track.Points.Add(p);
                    }
                }
            }
        }
    }
}
