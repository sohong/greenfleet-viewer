////////////////////////////////////////////////////////////////////////////////
// RepositoryViewModel.cs
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
using System.ComponentModel.Composition;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Viewer.Common;
using Viewer.Common.Service;
using Viewer.Personal.View;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

namespace Viewer.Personal.ViewModel
{
    /// <summary>
    /// 리파지토리 검색 방법
    /// </summary>
    public enum SearchMode
    {
        Range,      // 구간 설정
        //Today,      // 오늘
        //TwoDays,    // 이틀
        Recent,     // 최근
        RecentTwo   // 최근 이틀
    }


    /// <summary>
    /// View model base for LocalRepositoryViewModel and DeviceRepositoryViewModel.
    /// </summary>
    [Export]
    public class RepositoryViewModel : ViewModelBase
    {
        #region fields

        private DeviceRepository m_deviceRepository;
        private ListCollectionView m_vehicles;
        private bool m_loading;
        private ListCollectionView m_deviceTracks;
        private ListCollectionView m_localTracks;
        private TrackCollection m_selectedTracks;
        private PlaybackManager m_playbackManager;

        #endregion // fields


        #region constructors

        public RepositoryViewModel()
        {
            this.DriveManager = new DriveManager();
            m_deviceRepository = new DeviceRepository();

            m_vehicles = new ListCollectionView(PersonalDomain.Domain.Vehicles);
            m_vehicles.CurrentChanged += new EventHandler(Vehicles_CurrentChanged);

            m_selectedTracks = new TrackCollection();
            m_playbackManager = new PlaybackManager(m_selectedTracks);

            SearchFrom = DateTime.Today;
            SearchTo = DateTime.Today + TimeSpan.FromMinutes(23 * 60 + 59);
            SearchAll = true;
            SearchMode = SearchMode.Recent;
            AutoPlay = true;
            AllPlay = true;

            OpenCommand = new DelegateCommand<object>(DoOpen, CanOpen);
            SearchCommand = new DelegateCommand<object>(DoSearch, CanSearch);
            PlaybackCommand = new DelegateCommand(DoPlayback, CanPlayback);
            SaveCommand = new DelegateCommand(DoSave, CanSave);

            RegisterGlobalEvents();
        }

        private void RegisterGlobalEvents()
        {
            IEventAggregator events = ServiceLocator.Current.GetService(typeof(IEventAggregator)) as IEventAggregator;
            if (events != null) {
                events.GetEvent<TrackActivatedEvent>().Subscribe((track) => {
                    track.IsChecked = true;
                    ActiveTrack = track;
                });
                events.GetEvent<TrackDeactivatedEvent>().Subscribe((track) => {
                    track = m_playbackManager.GetNext(track, AllPlay, LoopPlay);
                    ActiveTrack = track;
                });
                events.GetEvent<TrackPointChangedEvent>().Subscribe((point) => {
                    this.TrackPoint = point;
                });
            }
        }

        #endregion // constructors


        #region properties

        public int ViewIndex
        {
            get { return m_viewIndex; }
            set
            {
                if (value != m_viewIndex) {
                    m_viewIndex = value;
                    RaisePropertyChanged(() => ViewIndex);
                    IsLocalChanged();
                }
            }
        }
        private int m_viewIndex;

        public bool IsLocal
        {
            get { return ViewIndex == 1; }
        }

        public ListCollectionView Vehicles
        {
            get { return m_vehicles; }
        }

        public IDriveManager DriveManager
        {
            get;
            set;
        }

        public DeviceRepository DeviceRepository
        {
            get { return m_deviceRepository; }
        }

        public LocalRepository LocalRepository
        {
            get { return PersonalDomain.Domain.Repository; }
        }

        public ListCollectionView Tracks
        {
            get { return m_deviceTracks; }
        }

        public TrackGroup DeviceTrackGroup
        {
            get { return m_deviceTrackGroup; }
            set
            {
                if (value != m_deviceTrackGroup) {
                    m_deviceTrackGroup = value;
                    RaisePropertyChanged(() => DeviceTrackGroup);
                }
            }
        }
        private TrackGroup m_deviceTrackGroup;

        public TrackGroup LocalTrackGroup
        {
            get { return m_localTrackGroup; }
            set
            {
                if (value != m_localTrackGroup) {
                    m_localTrackGroup = value;
                    RaisePropertyChanged(() => LocalTrackGroup);
                }
            }
        }
        private TrackGroup m_localTrackGroup;

        public TrackCollection SelectedTracks
        {
            get { return m_selectedTracks; }
        }

        /// <summary>
        /// 현재 선택되어 있는 vehicle.
        /// 선택 변경이 command들의 parameter에 반영될 수 있도록 setter를 작성한다.
        /// </summary>
        public Vehicle SelectedVehicle
        {
            get { return m_selectedVehicle; }
            set
            {
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
        public DateTime SearchFrom
        {
            get { return m_searchFrom; }
            set
            {
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
        public DateTime SearchTo
        {
            get { return m_searchTo; }
            set
            {
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
        public bool SearchAll
        {
            get { return m_searchAll; }
            set
            {
                if (value != m_searchAll) {
                    m_searchAll = value;
                    RaisePropertyChanged(() => SearchAll);
                }
            }
        }
        private bool m_searchAll;

        /// <summary>
        /// Search mode.
        /// </summary>
        public SearchMode SearchMode
        {
            get { return m_searchMode; }
            set
            {
                if (value != m_searchMode) {
                    m_searchMode = value;
                    RaisePropertyChanged(() => SearchMode);
                }
            }
        }
        private SearchMode m_searchMode;

        /// <summary>
        /// ActiveTrack이 설정되면 자동으로 재생할 것인 지 설정.
        /// </summary>
        public bool AutoPlay
        {
            get { return m_autoPlay; }
            set
            {
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
        public bool LoopPlay
        {
            get { return m_loopPlay; }
            set
            {
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
        public bool AllPlay
        {
            get { return m_allPlay; }
            set
            {
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
        public Track ActiveTrack
        {
            get { return m_activeTrack; }
            set
            {
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

        /// <summary>
        /// 재생 포인트.
        /// </summary>
        public TrackPoint TrackPoint
        {
            get { return m_trackPoint; }
            set
            {
                if (value != m_trackPoint) {
                    m_trackPoint = value;
                    RaisePropertyChanged(() => TrackPoint);
                }
            }
        }
        private TrackPoint m_trackPoint;

        public Commands Commands
        {
            get { return Commands.Instance; }
        }

        public ICommand OpenCommand
        {
            get;
            private set;
        }

        public ICommand SearchCommand
        {
            get;
            private set;
        }

        public ICommand PlaybackCommand
        {
            get;
            set;
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        #endregion // properties


        #region methods
        #endregion // methods


        #region internal properties

        protected bool IsLoading
        {
            get { return m_loading; }
        }

        #endregion // internal properties


        #region internal methods

        private void IsLocalChanged()
        {
            ActiveTrack = null;
            TrackPoint = null;
            // 시작 시점에는 아직 생성되지 않았을 수 있다.
            if (DeviceTrackGroup != null) {
                DeviceTrackGroup.IsChecked = false;
            }
            if (LocalTrackGroup != null) {
                LocalTrackGroup.IsChecked = false;
            }
        }

        protected void BeginLoading()
        {
            m_loading = true;
        }

        protected void EndLoading()
        {
            m_loading = false;
        }

        private void ResetDeviceTrackGroup(ListCollectionView tracks)
        {
            if (tracks != null)
                m_deviceTracks = tracks;

            this.DeviceTrackGroup = CreateGroupsFromTracks(m_deviceTracks);
            RegisterDeviceEvents(this.DeviceTrackGroup);
        }

        private void ResetLocalTrackGroup(ListCollectionView tracks)
        {
            if (tracks != null)
                m_localTracks = tracks;

            this.LocalTrackGroup = CreateGroupsFromTracks(m_localTracks);
            RegisterLocalEvents(this.LocalTrackGroup);
        }

        /// <summary>
        /// 트랙 목록으로부터 트랙 그룹 hierarchy를 생성한다.
        /// 트랙들은 CreateDate 순서로 정렬되었다고 가정한다.
        /// </summary>
        /// <param name="tracks"></param>
        /// <returns></returns>
        private TrackGroup CreateGroupsFromTracks(ListCollectionView tracks)
        {
            if (tracks.IsEmpty)
                return null;

            IList<TrackGroup> groups = new List<TrackGroup>();
            TrackGroup group = null;

            for (int i = 0, count = tracks.Count; i < count; i++) {
                Track track = (Track)tracks.GetItemAt(i);

                if (group == null || track.CreateDate.Hour != group.Date.Hour) {
                    group = new TrackGroup(track.CreateDate, TrackGroupLevel.Hour);
                    groups.Add(group);
                }

                group.Add(track);
            }

            TrackGroup root = CreateGroupHierarchy(groups);
            return root;
        }

        private TrackGroup CreateGroupHierarchy(IList<TrackGroup> groups)
        {
            IList<TrackGroup> parents = new List<TrackGroup>();
            TrackGroup parent = CreateParent(groups[0]);
            parent.Add(groups[0]);
            parents.Add(parent);

            for (int i = 1, count = groups.Count; i < count; i++) {
                if (!Containable(parent, groups[i])) {
                    parent = CreateParent(groups[i]);
                    parents.Add(parent);
                }
                parent.Add(groups[i]);
            }

            return parents.Count > 1 ? CreateGroupHierarchy(parents) : parent;
        }

        private TrackGroup CreateParent(TrackGroup group)
        {
            TrackGroupLevel level = (TrackGroupLevel)(group.Level + 1);
            TrackGroup parent = new TrackGroup(group.Date, level);
            return parent;
        }

        private bool Containable(TrackGroup parent, TrackGroup group)
        {
            switch (parent.Level) {
                case TrackGroupLevel.Day:
                    return group.Date.Day == parent.Date.Day;
                case TrackGroupLevel.Month:
                    return group.Date.Month == parent.Date.Month;
                case TrackGroupLevel.Year:
                    return group.Date.Year == parent.Date.Year;
            }

            return true;
        }

        private TrackGroup CreateRoot(Track startTrack, Track endTrack)
        {
            TrackGroupLevel level;
            DateTime dStart = startTrack.CreateDate;
            DateTime dEnd = endTrack.CreateDate;

            if (dStart.Year != dEnd.Year) {
                level = TrackGroupLevel.All;
            } else if (dStart.Month != dEnd.Month) {
                level = TrackGroupLevel.Year;
            } else if (dStart.Day != dEnd.Day) {
                level = TrackGroupLevel.Month;
            } else {
                level = TrackGroupLevel.Day;
            }

            TrackGroup root = new TrackGroup(startTrack.CreateDate, level);
            return root;
        }

        private void RegisterDeviceEvents(TrackGroup group)
        {
            group.TrackChanged += new Action<TrackGroup, Track, string>(TrackGroup_TrackChanged);
            group.TrackAllChanged += new Action<TrackGroup>(DeviceTrackGroup_TrackAllChanged);
            foreach (object child in group.Children) {
                if (child is TrackGroup) {
                    RegisterDeviceEvents((TrackGroup)child);
                }
            }
        }

        private void RegisterLocalEvents(TrackGroup group)
        {
            group.TrackChanged += new Action<TrackGroup, Track, string>(TrackGroup_TrackChanged);
            group.TrackAllChanged += new Action<TrackGroup>(LocalTrackGroup_TrackAllChanged);
            foreach (object child in group.Children) {
                if (child is TrackGroup) {
                    RegisterLocalEvents((TrackGroup)child);
                }
            }
        }

        private void TrackGroup_TrackChanged(TrackGroup group, Track track, string propName)
        {
            // 현재 재생 중인 것은 그냥 놔둔다.
            if (track == ActiveTrack && !track.IsChecked) {
                track.IsChecked = true;
            }

            if (track.IsChecked && !m_selectedTracks.Contains(track)) {
                m_selectedTracks.Add(track);
            } else if (!track.IsChecked && m_selectedTracks.Contains(track)) {
                m_selectedTracks.Remove(track);
            }
        }

        private void ResetTrackSelection(ListCollectionView tracks)
        {
            if (tracks != null) {
                m_selectedTracks.BeginUpdate();
                try {
                    m_selectedTracks.Clear();
                    foreach (Track track in tracks) {
                        if (track.IsChecked) {
                            m_selectedTracks.Add(track);
                        }
                    }

                } finally {
                    m_selectedTracks.EndUpdate();
                }
            }
        }

        private void DeviceTrackGroup_TrackAllChanged(TrackGroup group)
        {
            ResetTrackSelection(m_deviceTracks);
        }

        private void LocalTrackGroup_TrackAllChanged(TrackGroup group)
        {
            ResetTrackSelection(m_localTracks);
        }

        private void Vehicles_CurrentChanged(object sender, EventArgs e)
        {
            IsLocalChanged();
            SelectedVehicle = Vehicles.CurrentItem as Vehicle;
            CheckCommands();
        }

        protected virtual void CheckCommands()
        {
            //((DelegateCommand<object>)DeleteCommand).RaiseCanExecuteChanged();
            //CommandManager.InvalidateRequerySuggested();
        }

        // Open command
        private bool CanOpen(object data)
        {
            return true;
        }

        private void DoOpen(object data)
        {
            string folder = DriveManager.FindTrackDrive(PersonalDomain.Domain.Preferences.Testing);
            if (folder != null) {
                BeginLoading();
                try {
                    DeviceRepository.Open(SelectedVehicle, folder, () => {
                        ResetDeviceTrackGroup(DeviceRepository.GetTracks());
                        if (this.DeviceTrackGroup != null) {
                            //this.SearchFrom = DeviceRepository.StartTime.StripSeconds();
                            //this.SearchTo = DeviceRepository.EndTime.StripSeconds();
                        }

                        // for testing
                        if (data is Action) {
                            ((Action)data)();
                        }
                    });

                } finally {
                    EndLoading();
                }

            } else {
                //PersonalDomain.Domain.EventAggregator.GetEvent<NoDriveEvent>().Publish(null);
                // TODO 테스트 가능하도록 MessageUtil을 서비스 인터페이스로 구현해야 한다.
                MessageUtil.Show("트랙 데이터 드라이브가 존재하지 않습니다.");
            }
        }

        // Search command
        private bool CanSearch(object data)
        {
            return true;
        }

        private void DoSearch(object data)
        {
            DateTime dateFrom = SearchFrom;
            DateTime dateTo = SearchTo;

            switch (SearchMode) {
            /*
            case ViewModel.SearchMode.Today:
                dateFrom = DateTime.Today;
                dateTo = dateFrom + TimeSpan.FromMinutes(23 * 60 + 59);
                break;

            case ViewModel.SearchMode.TwoDays:
                dateFrom = DateTime.Today - TimeSpan.FromDays(1);
                dateTo = dateFrom + TimeSpan.FromMinutes(47 * 60 + 59);
                break;
            */
            case ViewModel.SearchMode.Recent:
                dateFrom = new TrackFolderManager(LocalRepository).GetRecentDay(SelectedVehicle);
                dateTo = dateFrom + TimeSpan.FromMinutes(23 * 60 + 59);
                break;

            case ViewModel.SearchMode.RecentTwo:
                dateFrom = new TrackFolderManager(LocalRepository).GetRecentDay(SelectedVehicle) - TimeSpan.FromDays(1); ;
                dateTo = dateFrom + TimeSpan.FromMinutes(47 * 60 + 59);
                break;

            case ViewModel.SearchMode.Range:
            default:
                break;
            }

            SearchFrom = dateFrom;
            SearchTo = dateTo;

            LocalRepository.Find(SelectedVehicle, dateFrom, dateTo, () => {
                ResetLocalTrackGroup(LocalRepository.GetTracks());
            });
        }

        // Playback command
        private bool CanPlayback()
        {
            return true;
        }

        private void DoPlayback()
        {
            Track track = m_playbackManager.GetFirst();
            ActiveTrack = track;
        }

        // Save(sd card -> local storage) command
        private bool CanSave()
        {
            return true;
        }

        private void DoSave()
        {
            SaveViewModel model = new SaveViewModel(DeviceRepository, SearchFrom, SearchTo);
            DialogService.Run("저장", new SaveView(), model);
        }

        #endregion // internal methods
    }
}
