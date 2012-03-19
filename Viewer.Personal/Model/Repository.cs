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

        public static bool ParseTrackFile(string fileName, ref DateTime date) {
            Match match = Repository.TRACK_DATE_PATTERN.Match(fileName);
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

        private ObservableCollection<Track> m_tracks;
        private string m_rootPath;
        private TrackFolderManager m_folderManager;
        private TrackImportHelper m_importHelper;
        
        #endregion // fields


        #region constructors

        public Repository() {
            m_tracks = new ObservableCollection<Track>();
            m_folderManager = new TrackFolderManager(this);
            m_importHelper = new TrackImportHelper(this);
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
        /// track file이 저장될 폴더명을 리턴한다.
        /// relative가 true이면 repository root 상대 경로로 리턴한다.
        /// 기존하지 않으면 생성한 후 리턴한다.
        /// </summary>
        public string GetFolder(string trackFile, bool relative) {
            return m_folderManager.GetFolder(trackFile, relative);
        }

        /// <summary>
        /// 외부 트랙파일들을 스토리지의 각 위치에 추가한다.
        /// files에는 확장자 없는 파일명들이 들어있다.
        /// </summary>
        public void ImportTrackFiles(IEnumerable<string> files, bool overwrite) {
            LogUtil.Debug("Import track files...");
            m_importHelper.Import(files, overwrite);
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
