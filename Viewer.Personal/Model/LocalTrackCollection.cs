////////////////////////////////////////////////////////////////////////////////
// LocalTrackCollection.cs
// 2012.04.12, created by sohong
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
using Viewer.Common.Model;
using Viewer.Common.ViewModel;
using Viewer.Common.Service;
using System.ComponentModel;
using System.Windows;
using Viewer.Common.Loader;
using System.Collections.ObjectModel;
using System.Windows.Data;

namespace Viewer.Personal.Model
{
    /// <summary>
    /// Local pc에 저장된 트랙파일들을 읽어 Track 컬렉션에 추가한다.
    /// </summary>
    public class LocalTrackCollection
    {
        #region fields

        private ObservableCollection<Track> m_tracks;
        private LocalTrackLoader m_loader;
        private DateTime m_startTime;
        private DateTime m_endTime;

        #endregion // fields


        #region constructor

        public LocalTrackCollection()
        {
            m_tracks = new ObservableCollection<Track>();
            m_loader = new LocalTrackLoader();
        }

        #endregion // constructor


        #region properties

        public int Count
        {
            get { return m_tracks.Count; }
        }

        public IEnumerable<Track> Tracks
        {
            get { return m_tracks; }
        }

        public DateTime StartTime
        {
            get { return m_startTime; }
        }

        public DateTime EndTime
        {
            get { return m_endTime; }
        }

        #endregion // properties


        #region methods

        public void Clear()
        {
            m_tracks.Clear();
        }

        /// <summary>
        /// 모든 track을 check되지 않은 상태로 변경한다.
        /// </summary>
        public void ClearSelection()
        {
            foreach (Track t in m_tracks) {
                t.IsChecked = false;
            }
        }

        public void Load(IEnumerable<string> files, int count, string title, Action callback)
        {
            ProgressViewModel progView = new ProgressViewModel();
            progView.Maximum = count;
            progView.Caption = "트랙 파일들을 로드합니다.";
            DialogService.RunProgress(title, progView);

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
                        if (track.CreateDate > m_endTime)
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

        public ListCollectionView GetTracks()
        {
            ListCollectionView view = new ListCollectionView(m_tracks);
            view.SortDescriptions.Add(new SortDescription("CreateDate", ListSortDirection.Ascending));
            return view;
        }

        public IEnumerable<Track> GetSelection()
        {
            return m_tracks.Where((t) => t.IsChecked == true);
        }

        /// <summary>
        /// 트랙 정보를 제거한다.
        /// </summary>
        /// <param name="track"></param>
        public bool Delete(Track track)
        {
            int index = m_tracks.IndexOf(track);
            if (index >= 0) {
                m_tracks.RemoveAt(index);
                return true;
            }
            return false;
        }

        #endregion // methods
    }
}
