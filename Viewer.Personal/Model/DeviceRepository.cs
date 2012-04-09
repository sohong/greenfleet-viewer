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
using Viewer.Common.Loader;
using System.ComponentModel;
using System.Windows;
using Viewer.Common;
using Viewer.Common.ViewModel;
using Viewer.Common.Service;

namespace Viewer.Personal.Model {

    /// <summary>
    /// SD Card 등 블랙박스 저장 미디어에 포함된 트랙 정보들.
    /// </summary>
    public class DeviceRepository : NotificationObjectEx {

        #region fields

        private Vehicle m_vehicle;
        private string m_rootPath;
        private ObservableCollection<Track> m_tracks;
        private LocalTrackLoader m_loader;
        private DateTime m_startTime;
        private DateTime m_endTime;
        
        #endregion // fields


        #region constructor

        public DeviceRepository() {
            m_loader = new LocalTrackLoader();
        }

        #endregion // constructor


        #region properties

        public Vehicle Vehicle {
            get { return m_vehicle; }
        }

        public string RootPath {
            get { return m_rootPath; }
        }

        public IEnumerable<Track> Tracks {
            get { return m_tracks; }
        }

        public IEnumerable<Track> Selection {
            get {
                if (m_tracks != null) {
                    return m_tracks.Where((t) => t.IsChecked == true);
                }
                return null;
            }
        }

        public int TrackCount {
            get { return m_tracks != null ? m_tracks.Count : 0; }
        }

        /// <summary>
        /// 포함된 트랙들 중 가장 먼저인 놈의 파일 표시 시간값.
        /// 즉, Track.CreateDate.
        /// event_2012_03_11_20_38_31.inc => 2012-03-11 20:38:31
        /// </summary>
        public DateTime StartTime {
            get { return m_startTime; }
        }

        /// <summary>
        /// 포함된 트랙들 중 가장 나중인 놈의 파일 표시 시간값.
        /// </summary>
        public DateTime EndTime {
            get { return m_endTime; }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// 입력 디바이스의 트랙 목록을 읽어들인다.
        /// 시작/끝 일시를 계산한다.
        /// </summary>
        /// <param name="rootPath"></param>
        public void Open(Vehicle vehicle, string rootPath, Action callback) {
            m_vehicle = vehicle;
            m_rootPath = rootPath;

            if (m_tracks != null) {
                m_tracks.Clear();
                m_tracks = null;
            }
            if (Directory.Exists(rootPath)) {
                m_tracks = new ObservableCollection<Track>();

                LoadTracks(callback);
            }
        }

        /// <summary>
        /// Track 컬렉션 원본에 대한 뷰를 생성한다.
        /// </summary>
        public ListCollectionView GetTracks() {
            ListCollectionView view = new ListCollectionView(m_tracks);
            view.SortDescriptions.Add(new SortDescription("CreateDate", ListSortDirection.Ascending));
            return view;
        }

        /// <summary>
        /// 트랙 목록으로부터 트랙 그룹 hierarchy를 생성한다.
        /// </summary>
        /// <param name="tracks"></param>
        /// <returns></returns>
        public TrackGroup CreateGroupsFromTracks(ListCollectionView tracks) {
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

        /// <summary>
        /// 모든 track을 check되지 않은 상태로 변경한다.
        /// </summary>
        public void ClearSelection() {
            foreach (Track t in m_tracks) {
                t.IsChecked = false;
            }
        }

        #endregion // methods


        #region internal methods

        private ProgressViewModel CreateProgressView(int total) {
            ProgressViewModel progView = new ProgressViewModel();
            progView.Maximum = total;
            return progView;
        }

        private void LoadTracks(Action callback) {
            string[] files = Directory.GetFiles(m_rootPath, "*.inc");
            if (files.Length > 0) {
                ProgressViewModel progView = CreateProgressView(files.Length);
                progView.Caption = "트랙 파일들을 로드합니다.";
                DialogService.RunProgress("SD 카드 로딩", progView);

                int cnt = 0;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (sender, e) => {
                    m_startTime = DateTime.MaxValue;
                    m_endTime = DateTime.MinValue;

                    foreach (string file in files) {
                        if (Application.Current != null && progView.IsCanceled) {
                            Application.Current.Dispatcher.Invoke((Action)(() => {
                                m_tracks.Clear();
                            }));
                            break;
                        }

                        cnt++;
                        Track track = m_loader.Load(file, false);
                        if (track != null) {
                            if (Application.Current != null) {
                                Application.Current.Dispatcher.Invoke((Action)(() => {
                                    m_tracks.Add(track);
                                }));
                            } else {
                                m_tracks.Add(track);
                            }

                            if (track.CreateDate < m_startTime)
                                m_startTime = track.CreateDate;
                            if (track.CreateDate > m_startTime)
                                m_endTime = track.CreateDate;
                        }

                        //worker.ReportProgress(++cnt);

                        if (Application.Current != null) {
                            Application.Current.Dispatcher.Invoke((Action)(() => {
                                progView.Value = cnt;
                                progView.Message = file;
                            }));
                        }
                    }
                };
                //worker.ProgressChanged += (sender, e) => { };
                worker.RunWorkerCompleted += (sender, e) => {
                    if (callback != null)
                        callback();
                };
                //worker.WorkerReportsProgress = true;
                worker.RunWorkerAsync();
            }
        }

        #endregion // interanl methods
    }
}
