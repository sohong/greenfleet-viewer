////////////////////////////////////////////////////////////////////////////////
// DeviceRepository.cs
// 2012.03.20, created by sohong
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
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.IO;
using Viewer.Common.Model;

namespace Viewer.Personal.Model {

    /// <summary>
    /// SD Card 등 블랙박스 저장 미디어에 포함된 트랙 정보들.
    /// </summary>
    public class DeviceRepository {

        #region fields

        private Vehicle m_vehicle;
        private string m_rootPath;
        private ObservableCollection<Track> m_tracks;
        
        #endregion // fields


        #region constructor

        public DeviceRepository() {
        }

        #endregion // constructor


        #region properties

        public string RootPath {
            get { return m_rootPath; }
        }

        public int TrackCount {
            get { return m_tracks != null ? m_tracks.Count : 0; }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// 입력 디바이스의 트랙 목록을 읽어들인다.
        /// </summary>
        /// <param name="rootPath"></param>
        public void Open(Vehicle vehicle, string rootPath) {
            m_vehicle = vehicle;
            m_rootPath = rootPath;

            if (m_tracks != null) {
                m_tracks.Clear();
                m_tracks = null;
            }
            if (Directory.Exists(rootPath)) {
                m_tracks = new ObservableCollection<Track>();
                LoadTracks();
            }
        }

        /// <summary>
        /// Track 컬렉션 원본에 대한 뷰를 생성한다.
        /// </summary>
        public ListCollectionView GetTracks() {
            return new ListCollectionView(m_tracks);
        }

        public TrackGroup LoadGroups(ListCollectionView tracks) {
            if (tracks.Count > 0) {
                Track track = (Track)tracks.GetItemAt(0);
                TrackGroup root = new TrackGroup(track.CreateDate, TrackGroupLevel.Day);
                DateTime curr = track.CreateDate.Date;

                TrackGroup group = null;
                foreach (Track tr in tracks) {
                    DateTime d = tr.CreateDate;
                    if (d.Date == root.Date.Date) {
                        if (group == null || d.Hour != curr.Hour) {
                            group = new TrackGroup(d, TrackGroupLevel.Hour);
                            root.Add(group);
                        }

                        group.Add(tr);
                        curr = d;
                    }
                }

                return root;
            }

            return null;
        }

        #endregion // methods


        #region internal methods

        private void LoadTracks() {
            string[] files = Directory.GetFiles(m_rootPath, "*.inc");
            foreach (string file in files) {
                string name = Path.GetFileNameWithoutExtension(file);
                DateTime d = new DateTime();
                if (Repository.ParseTrackFile(name, ref d)) {
                    Track track = new Track();
                    track.TrackType = name.StartsWith("event") ? TrackType.Event : TrackType.All;
                    track.CreateDate = d;

                    m_tracks.Add(track);
                }
            }
        }

        #endregion // interanl methods
    }
}
