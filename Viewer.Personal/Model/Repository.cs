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

namespace Viewer.Personal.Model {
    
    /// <summary>
    /// Track 파일 저장소.
    /// </summary>
    public class Repository {

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

        public ListCollectionView GetTracks() {
            return new ListCollectionView(m_tracks);
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
