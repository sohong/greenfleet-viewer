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

namespace Viewer.Common.Loader
{
    /// <summary>
    /// local pc에 저장되어 있는 track 파일을 읽어 track 개체를 생성한다.
    /// </summary>
    public class LocalTrackLoader : TrackLoaderBase, ITrackLoader
    {
        #region consts

        private const string DATE_FORMAT = "yyyy_MM_dd_HH_mm_ss";

        public static DateTime ParseDate(string s)
        {
            DateTime date = DateTime.ParseExact(s, DATE_FORMAT, null);
            return date;
        }

        #endregion // consts


        #region static members

        public static bool FileToDateTime(string fileName, out DateTime date, out TrackType trackType)
        {
            string s = Path.GetFileNameWithoutExtension(fileName);
            date = DateTime.MinValue;
            trackType = TrackType.All;

            if (s.StartsWith("all_")) {
                s = s.Substring(4);
            } else if (s.StartsWith("event_")) {
                s = s.Substring(6);
                trackType = TrackType.Event;
            } else {
                return false;
            }

            date = ParseDate(s);
            return true;
        }

        #endregion // static members


        #region constructor

        public LocalTrackLoader()
        {
        }

        #endregion // constructor


        #region ITrackLoader

        public Track Load(object source, bool convertVideo)
        {
            string path = source.ToString();
            Track track = LoadTrack(path, convertVideo);
            return track;
        }

        #endregion // ITrackLoader


        #region internal methods

        private Track LoadTrack(string path, bool convertVideo)
        {
            if (path.Contains("13_13_")) {
                Debug.WriteLine(path);
            }

            // track 정보
            Track track = CreateTrack(path, convertVideo);

            // track points
            using (StreamReader reader = new StreamReader(path)) {
                LoadPoints(track, reader);
            }

            return track;
        }

        protected Track CreateTrack(string path, bool convertVideo)
        {
            if (!File.Exists(path)) {
                return null;
            }

            // create date
            TrackType tt;
            DateTime d;
            if (!FileToDateTime(path, out d, out tt))
                return null;

            Track track = new Track();

            // track file
            track.TrackFile = path;
            // create date
            track.CreateDate = d;
            // track type
            track.TrackType = tt;
            // video file
            string s = Path.ChangeExtension(path, "264");
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
