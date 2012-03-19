////////////////////////////////////////////////////////////////////////////////
// TrackCatalog.cs
// 2012.03.19, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Viewer.Common.Model;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Track file index.
    /// 차량/월 단위로 관리한다.
    /// </summary>
    public class TrackCatalog {

        #region fields

        private Vehicle m_vehicle;
        private int m_year;
        private int m_month;
        private List<Track> m_tracks;
        
        #endregion // fields


        #region constructor

        public TrackCatalog(Vehicle vehicle, int year, int month) {
            m_vehicle = vehicle;
            m_year = year;
            m_month = month;
        }

        #endregion // constructor


        #region properties
        #endregion // properties


        #region methods

        public void Load(string catalogPath) {
            m_tracks = new List<Track>();
        }

        public void Save(string catalogPath) {
        }

        #endregion // methods


        #region internal methods

        /// <summary>
        /// Track file을 읽어 Track 개체를 생성한다.
        /// </summary>
        /// <param name="fileName">경로와 확장자가 제외된 파일명.</param>
        private Track Add(string fileName) {
            DateTime date = new DateTime();
            if (Repository.ParseTrackFile(fileName, ref date)) {
                if (date.Year == m_year && date.Month == m_month) {
                    Track track = new Track();
                    track.VehicleId = m_vehicle.VehicleId;
                    track.CreateDate = date;
                    if (m_tracks != null) {
                        m_tracks.Add(track);
                    }
                    return track;
                }
            }
            return null;
        }

        #endregion // internal methods
    }
}
