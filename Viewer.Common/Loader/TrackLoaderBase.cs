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

        #region consts

        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";

        #endregion // consts


        #region internal methods

        /// <summary>
        /// .inc 파일을 읽어 각 라인을 TopicPoint로 생성하고,
        /// track에 추가한다.
        /// </summary>
        /// <param name="track"></param>
        /// <param name="reader"></param>
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
                        p.PointTime = DateTime.ParseExact(items[0], DATE_FORMAT, null);
                        p.Latitude = Convert.ToDouble(items[1]);
                        p.Longitude = Convert.ToDouble(items[2]);
                        p.Velocity = Convert.ToDouble(items[3]);
                        p.AccelerationX = Convert.ToDouble(items[4]);
                        p.AccelerationY = Convert.ToDouble(items[5]);
                        p.AccelerationZ = Convert.ToDouble(items[6]);

                        track.AddPoint(p);
                    }
                }
            }
        }

        #endregion // internal methods
    }
}
