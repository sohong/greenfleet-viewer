////////////////////////////////////////////////////////////////////////////////
// DeviceRepositoryViewModel.cs
// 2012.03.20, created by sohong
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
using Viewer.Personal.Model;
using System.Windows.Data;
using System.IO;
using Viewer.Personal.Command;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Viewer.Common.Model;
using Viewer.Common.Util;
using Viewer.Common.Event;
using Viewer.Personal.Event;
using System.Collections.ObjectModel;
using Viewer.Common.Service;
using Viewer.Personal.View;
using Viewer.Common;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for DeviceRepositoryView
    /// </summary>
    public class DeviceRepositoryViewModel : RepoViewModelBase, ITrackStateObserver {

        #region fields

        private DeviceRepository m_repository;
        private ListCollectionView m_tracks;
        private TrackCollection m_selectedTracks;

        #endregion // fields


        #region constructors

        public DeviceRepositoryViewModel() {
            this.DriveManager = new DriveManager();
            m_repository = new DeviceRepository();
            m_selectedTracks = new TrackCollection();

            SearchFrom = DateTime.Today;
            SearchTo = DateTime.Today + TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59);
            SearchAll = true;
            AutoPlay = true;

            LoadCommand = new DelegateCommand<object>(DoLoad, CanLoad);
            SearchCommand = new DelegateCommand(DoSearch, CanSearch);
            ConfigDeviceCommand = new DelegateCommand<object>(DoConfigDevice, CanConfigDevice);
        }

        #endregion // constructors


        #region ITrackStateObserver

        public void TrackChanged(Track track, string propName) {
            // 현재 재생 중인 것은 그냥 놔둔다.
            if (track == ActiveTrack && !track.IsChecked) {
                track.IsChecked = true;
            }

            if (track.IsChecked) {
                m_selectedTracks.Add(track);
            } else {
                m_selectedTracks.Remove(track);
            }
        }

        #endregion // ITrackStateObserver


        #region properties

        public IDriveManager DriveManager {
            get;
            set;
        }

        public DeviceRepository Repository {
            get { return m_repository; }
        }

        public ListCollectionView Tracks {
            get { return m_tracks; }
        }

        public TrackCollection SelectedTracks {
            get { return m_selectedTracks; }
        }

        public ICommand LoadCommand {
            get;
            private set;
        }

        public ICommand SearchCommand {
            get;
            private set;
        }

        public ICommand ConfigDeviceCommand {
            get;
            private set;
        }

        public ICommand SaveCommand {
            get;
            private set;
        }

        #endregion // properties


        #region overriden methods

        protected override void CheckCommands() {
            base.CheckCommands();
        }

        #endregion // overriden methods


        #region internal methods

        // Load command
        private bool CanLoad(object data) {
            return true;
        }

        private void DoLoad(object data) {
            /*
            string folder = DialogUtil.SelectFolder("트랙 파일들이 저장된 위치를 선택하세요.", null);
            if (folder != null) {
                m_repository.Open(SelectedVehicle, folder);
                m_tracks = m_repository.GetTracks();
                this.TrackGroup = m_repository.LoadGroups(m_tracks);
            }
             */

            string folder = DriveManager.FindTrackDrive(PersonalDomain.Domain.Preferences.Testing);
            if (folder != null) {
                BeginLoading();
                try {
                    m_repository.Open(SelectedVehicle, folder);
                    m_tracks = m_repository.GetTracks();
                    this.TrackGroup = m_repository.CreateGroupsFromTracks(m_tracks);
                    this.TrackGroup.Observer = this;
                    this.SearchFrom = m_repository.StartTime.StripSeconds();
                    this.SearchTo = m_repository.EndTime.StripSeconds();
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
        private bool CanSearch() {
            return true;
        }

        private void DoSearch() {
            if (m_tracks != null) {
                //Repository.ClearSelection(); // 기존 선택들을 굳이 해제시킬 필요는 없을것 같다.

                m_tracks.Filter = (track) => {
                    DateTime d = ((Track)track).CreateDate.StripSeconds();
                    return d >= SearchFrom && d <= SearchTo;
                };
                this.TrackGroup = m_repository.CreateGroupsFromTracks(m_tracks);
            }
        }

        // Config device command
        private bool CanConfigDevice(object data) {
            return true;
        }

        private void DoConfigDevice(object data) {
            DialogService.Run("기기 설정", new DeviceConfigView(), new DeviceConfigViewModel());
        }

        // Save(sd card -> local storage) command
        private bool CanSave(object data) {
            return true;
        }

        private void DoSave(object data) {
        }

        #endregion // internal methods
    }
}
