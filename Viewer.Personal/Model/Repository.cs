////////////////////////////////////////////////////////////////////////////////
// Repository.cs
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
using Viewer.Common.Util;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.IO;
using System.Text.RegularExpressions;

namespace Viewer.Personal.Model {
    
    /// <summary>
    /// Track 파일 저장소.
    /// </summary>
    public class Repository {

        #region static members

        public static readonly Regex TRACK_DATE_PATTERN = new Regex(@"\d{4}_\d{2}_\d{2}_\d{2}_\d{2}_\d{2}");

        #endregion // static members


        #region fields

        private ObservableCollection<Track> m_tracks;
        private string m_rootPath;
        
        #endregion // fields


        #region constructors

        public Repository() {
            m_tracks = new ObservableCollection<Track>();
        }

        #endregion // constructors


        #region properties

        public string RootPath {
            get { return m_rootPath; }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// Repository를 연다.
        /// </summary>
        public void Open(string rootPath) {
            LogUtil.Info("Repository open...");

            m_rootPath = rootPath;
            if (!Directory.Exists(m_rootPath)) {
                Directory.CreateDirectory(m_rootPath);
            }

            LogUtil.Info("Repository opened.");
        }

        /// <summary>
        /// Track 컬렉션 원본에 대한 뷰를 생성한다.
        /// </summary>
        public ListCollectionView GetTracks() {
            return new ListCollectionView(m_tracks);
        }

        /// <summary>
        /// 외부 트랙파일들을 스토리지의 각 위치에 추가한다.
        /// files에는 확장자 없는 파일명들이 들어있다.
        /// </summary>
        public void ImportTrackFiles(IEnumerable<string> files) {
            LogUtil.Debug("Import track files...");
            TrackImportHelper.Import(files, this);
        }

        #endregion // methods


        #region internal methods

        private Track Find(DateTime time) {
            return null;
        }

        private IEnumerable<Track> LoadTracks(DateTime fromTime, DateTime toTime, bool inclusive) {
            return null;
        }

        private Track LoadTrack(DateTime time) {
            return null;
        }

        private void SaveTracks(IEnumerable<Track> tracks) {
        }

        private void SaveTrack(Track track) {
        }

        #endregion // internal methods
    }
}
