////////////////////////////////////////////////////////////////////////////////
// Repository.cs
// 2012.04.10, created by sohong
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
using Viewer.Common;
using System.Windows.Data;
using System.ComponentModel;
using Viewer.Common.Model;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace Viewer.Personal.Model
{
    /// <summary>
    /// Repository base.
    /// </summary>
    public abstract class Repository : NotificationObjectEx
    {
        #region static members

        public static readonly Regex TRACK_DATE_PATTERN = new Regex(@"\d{4}_\d{2}_\d{2}_\d{2}_\d{2}_\d{2}");

        public static bool ParseTrackFile(string fileName, ref DateTime date)
        {
            Match match = LocalRepository.TRACK_DATE_PATTERN.Match(fileName);
            if (match.Success) {
                string[] arr = match.Value.Split('_');
                if (arr.Length >= 6) {
                    date = new DateTime(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]),
                        int.Parse(arr[3]), int.Parse(arr[4]), int.Parse(arr[5]));
                    return true;
                }
            }
            return false;
        }

        #endregion // static members


        #region fields

        private string m_name;
        private LocalTrackCollection m_tracks;

        #endregion // fields


        #region constructor

        public Repository(string name)
        {
            m_name = name;
            m_tracks = new LocalTrackCollection();
        }

        #endregion // constructor


        #region properties

        public IEnumerable<Track> Tracks
        {
            get { return m_tracks.Tracks; }
        }

        public int TrackCount
        {
            get { return TrackList.Count; }
        }

        /// <summary>
        /// 포함된 트랙들 중 가장 먼저인 놈의 파일 표시 시간값.
        /// 즉, Track.CreateDate.
        /// event_2012_03_11_20_38_31.inc => 2012-03-11 20:38:31
        /// </summary>
        public DateTime StartTime
        {
            get { return TrackList.StartTime; }
        }

        /// <summary>
        /// 포함된 트랙들 중 가장 나중인 놈의 파일 표시 시간값.
        /// </summary>
        public DateTime EndTime
        {
            get { return TrackList.EndTime; }
        }

        public IEnumerable<Track> Selection
        {
            get { return TrackList.GetSelection(); }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// Track 컬렉션 원본에 대한 뷰를 생성한다.
        /// </summary>
        public ListCollectionView GetTracks()
        {
            return m_tracks.GetTracks();
        }

        /// <summary>
        /// 모든 track을 check되지 않은 상태로 변경한다.
        /// </summary>
        public void ClearSelection()
        {
            TrackList.ClearSelection();
        }

        public void Delete(Track track)
        {
        }

        public void Delete(TrackGroup group)
        {
        }

        #endregion // methods


        #region overriden methods

        public override string ToString()
        {
            return m_name + " Repository";
        }

        #endregion // overriden methods


        #region internal properties

        protected LocalTrackCollection TrackList
        {
            get { return m_tracks; }
        }

        #endregion // internal properties
    }
}
