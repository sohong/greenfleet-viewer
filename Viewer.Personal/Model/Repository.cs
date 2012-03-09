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

namespace Viewer.Personal.Model {
    
    /// <summary>
    /// Track 파일 저장소.
    /// </summary>
    public class Repository {

        #region fields

        private List<Track> m_tracks;
        
        #endregion // fields


        #region constructors

        public Repository() {
            m_tracks = new List<Track>();
        }

        #endregion // constructors


        #region properties
        #endregion // properties


        #region methods

        /// <summary>
        /// Repository를 연다.
        /// </summary>
        public void Open() {
            LogUtil.Info("Repository open...");

            LogUtil.Info("Repository opened.");
        }

        /// <summary>
        /// froTime에서 toTime까지의 track들을 가져온다.
        /// </summary>
        /// <param name="fromTime"></param>
        /// <param name="toTime"></param>
        /// <param name="inclusive">fromTime, toTime을 검색 범위에 포함시킬 것인가?</param>
        /// <returns></returns>
        public IEnumerable<Track> GetTracks(DateTime fromTime, DateTime toTime, bool inclusive = true) {
            return null;
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
