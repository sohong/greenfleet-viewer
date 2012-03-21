////////////////////////////////////////////////////////////////////////////////
// LocalTrackLoader.cs
// 2012.03.13, created by sohong
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
using System.Text.RegularExpressions;
using Viewer.Common.Model;
using Viewer.Common.Util;

namespace Viewer.Common.Loader {

    /// <summary>
    /// local pc에 저장되어 있는 track 파일을 읽어 track 개체를 생성한다.
    /// </summary>
    public class LocalTrackLoader : TrackLoaderBase, ITrackLoader {

        #region consts

        private const string DATE_FORMAT = "yyyy_MM_dd_HH_mm_ss";

        #endregion // consts

        
        #region constructor 

        public LocalTrackLoader() {
        }

        #endregion // constructor


        #region ITrackLoader

        public Track Load(object source, bool convertVideo) {
            string path = source.ToString();
            Track track = LoadTrack(path, convertVideo);
            return track;
        }

        #endregion // ITrackLoader


        #region internal methods

        private Track LoadTrack(string path, bool convertVideo) {
            // track 정보
            Track track = CreateTrack(path, convertVideo);

            // track points
            using (StreamReader reader = new StreamReader(path)) {
                LoadPoints(track, reader);
            }

            return track;
        }

        protected Track CreateTrack(string path, bool convertVideo) {
            if (!File.Exists(path)) {
                return null;
            }

            // create date
            TrackType tt = TrackType.All;
            string s = Path.GetFileNameWithoutExtension(path);
            if (s.StartsWith("all_")) {
                s = s.Substring(4);
            } else if (s.StartsWith("event_")) {
                s = s.Substring(6);
                tt = TrackType.Event;
            } else {
                return null;
            }
            DateTime d = DateTime.ParseExact(s, DATE_FORMAT, null);

            Track track = new Track();

            // create date
            track.CreateDate = d;
            // track type
            track.TrackType = tt;
            // video file
            s = Path.ChangeExtension(path, "264");
            if (File.Exists(s)) {
                track.VideoFile = s;
                if (convertVideo) {
                    track.MpegFile = VideoUtil.RawToMpeg(s, null);
                }
            }

            return track;
        }

        #endregion // internal methods
    }
}
