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

        public Track Load(object source) {
            string path = source.ToString();
            Track track = LoadTrack(path);
            return track;
        }

        #endregion // ITrackLoader


        #region internal methods

        private Track LoadTrack(string path) {
            // track 정보
            Track track = CreateTrack(path);

            // track points
            using (StreamReader reader = new StreamReader(path)) {
                LoadPoints(track, reader);
            }

            return track;
        }

        protected Track CreateTrack(string path) {
            if (!File.Exists(path)) {
                return null;
            }

            Track track = new Track();
            
            // video file
            string s = Path.ChangeExtension(path, "264");
            if (File.Exists(s)) {
                track.VideoFile = VideoUtil.RawToMpeg(s);
            }
            
            // create date
            s = Path.GetFileNameWithoutExtension(path);
            if (s.StartsWith("all_")) {
                s = s.Substring(4);
            } else { // "event_"
                s = s.Substring(6);
            }
            track.CreateDate = DateTime.ParseExact(s, DATE_FORMAT, null);

            return track;
        }

        #endregion // internal methods
    }
}
