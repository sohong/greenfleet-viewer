////////////////////////////////////////////////////////////////////////////////
// RepoViewModelBase.cs
// 2012.03.21, created by sohong
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
using Viewer.Common.ViewModel;
using System.Windows.Data;
using Viewer.Personal.Model;
using Viewer.Common.Model;
using Viewer.Common.Util;
using System.IO;
using Viewer.Personal.Command;
using Viewer.Common.Event;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// 리파지토리 검색 방법
    /// </summary>
    public enum SearchMode {
        Range,      // 구간 설정
        Today,      // 오늘
        TwoDays,    // 이틀
        Recent,     // 최근
        RecentTwo   // 최근 이틀
    }

    
    /// <summary>
    /// View model base for LocalRepositoryViewModel and DeviceRepositoryViewModel.
    /// </summary>
    public abstract class RepoViewModelBase : ViewModelBase {

        #region fields

        private ListCollectionView m_vehicles;
        private bool m_loading;

        #endregion // fields


        #region constructors

        public RepoViewModelBase() {
            m_vehicles = new ListCollectionView(PersonalDomain.Domain.Vehicles);
            m_vehicles.CurrentChanged += new EventHandler(Vehicles_CurrentChanged);
        }

        #endregion // constructors


        #region properties

        public ListCollectionView Vehicles {
            get { return m_vehicles; }
        }

        public TrackGroup TrackGroup {
            get { return m_trackGroup; }
            set {
                if (value != m_trackGroup) {
                    m_trackGroup = value;
                    RaisePropertyChanged(() => TrackGroup);
                }
            }
        }
        private TrackGroup m_trackGroup;

        /// <summary>
        /// 현재 선택되어 있는 vehicle.
        /// 선택 변경이 command들의 parameter에 반영될 수 있도록 setter를 작성한다.
        /// </summary>
        public Vehicle SelectedVehicle {
            get { return m_selectedVehicle; }
            set {
                if (value != m_selectedVehicle) {
                    m_selectedVehicle = value;
                    RaisePropertyChanged(() => SelectedVehicle);
                }
            }
        }
        private Vehicle m_selectedVehicle;

        /// <summary>
        /// 검색 조건 - 시작 시각(시/분), 혹은 시작 일시(일/시).
        /// </summary>
        public DateTime SearchFrom {
            get { return m_searchFrom; }
            set {
                if (value != m_searchFrom) {
                    m_searchFrom = value;
                    RaisePropertyChanged(() => SearchFrom);
                    if (!IsLoading) {
                        SearchAll = false;
                    }
                }
            }
        }
        private DateTime m_searchFrom;

        /// <summary>
        /// 검색 조건 - 끝 시각(시/분), 혹은 끝 일시(일/시).
        /// </summary>
        public DateTime SearchTo {
            get { return m_searchTo; }
            set {
                if (value != m_searchTo) {
                    m_searchTo = value;
                    RaisePropertyChanged(() => SearchTo);
                    if (!IsLoading) {
                        SearchAll = false;
                    }
                }
            }
        }
        private DateTime m_searchTo;

        /// <summary>
        /// 모두 가져오기
        /// </summary>
        public bool SearchAll {
            get { return m_searchAll; }
            set {
                if (value != m_searchAll) {
                    m_searchAll = value;
                    RaisePropertyChanged(() => SearchAll);
                }
            }
        }
        private bool m_searchAll;

        /// <summary>
        /// ActiveTrack이 설정되면 자동으로 재생할 것인 지 설정.
        /// </summary>
        public bool AutoPlay {
            get { return m_autoPlay; }
            set {
                if (value != m_autoPlay) {
                    m_autoPlay = value;
                    RaisePropertyChanged(() => AutoPlay);
                }
            }
        }
        private bool m_autoPlay;

        /// <summary>
        /// ActiveTrack을 반복 재생
        /// PlayAll이 true면 선택된 전체를 반복 재생.
        /// </summary>
        public bool LoopPlay {
            get { return m_loopPlay; }
            set {
                if (value != m_loopPlay) {
                    m_loopPlay = value;
                    RaisePropertyChanged(() => LoopPlay);
                }
            }
        }
        private bool m_loopPlay;

        /// <summary>
        /// ActiveTrack부터 선택된 모든 track을 차례대로 재생.
        /// LoopPlay가 true이면 전체 반복.
        /// </summary>
        public bool AllPlay {
            get { return m_allPlay; }
            set {
                if (value != m_allPlay) {
                    m_allPlay = value;
                    RaisePropertyChanged(() => AllPlay);
                }
            }
        }
        private bool m_allPlay;

        /// <summary>
        /// 현재 재생 중인 track.
        /// </summary>
        public Track ActiveTrack {
            get { return m_activeTrack; }
            set {
                if (value != m_activeTrack) {
                    m_activeTrack = value;

                    if (value != null && !string.IsNullOrWhiteSpace(value.VideoFile)) {
                        if (string.IsNullOrWhiteSpace(value.MpegFile) || !File.Exists(value.MpegFile)) {
                            value.MpegFile = VideoUtil.RawToMpeg(value.VideoFile, PersonalDomain.Domain.WorkingFolder);
                        }
                    }

                    RaisePropertyChanged(() => ActiveTrack);
                }
            }
        }
        private Track m_activeTrack;

        public Commands Commands {
            get { return Commands.Instance; }
        }

        #endregion // properties


        #region internal properties

        protected bool IsLoading {
            get { return m_loading; }
        }

        #endregion // internal properties


        #region internal methods

        protected void BeginLoading() {
            m_loading = true;
        }

        protected void EndLoading() {
            m_loading = false;
        }

        private void Vehicles_CurrentChanged(object sender, EventArgs e) {
            SelectedVehicle = Vehicles.CurrentItem as Vehicle;
            CheckCommands();
        }

        protected virtual void CheckCommands() {
            //((DelegateCommand<object>)DeleteCommand).RaiseCanExecuteChanged();
            //CommandManager.InvalidateRequerySuggested();
        }

        #endregion // internal methods
    }
}
