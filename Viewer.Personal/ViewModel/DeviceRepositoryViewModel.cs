﻿////////////////////////////////////////////////////////////////////////////////
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

namespace Viewer.Personal.ViewModel
{
    /// <summary>
    /// View model for DeviceRepositoryView
    /// </summary>
    public class DeviceRepositoryViewModel : RepositoryViewModelBase
    {
        #region fields

        private DeviceRepository m_repository;

        #endregion // fields


        #region constructors

        public DeviceRepositoryViewModel()
        {
            this.DriveManager = new DriveManager();
            m_repository = new DeviceRepository();

            SearchFrom = DateTime.Today;
            SearchTo = DateTime.Today + TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59);
            SearchAll = true;
            AutoPlay = true;

            LoadCommand = new DelegateCommand<object>(DoLoad, CanLoad);
            SearchCommand = new DelegateCommand(DoSearch, CanSearch);
            SaveCommand = new DelegateCommand(DoSave, CanSave);

            if (PersonalDomain.Domain.EventAggregator != null) {
                PersonalDomain.Domain.EventAggregator.GetEvent<DeviceTrackActivatedEvent>().Subscribe((track) => {
                    track.IsChecked = true;
                    ActiveTrack = track;
                });

                PersonalDomain.Domain.EventAggregator.GetEvent<TrackPointChangedEvent>().Subscribe((point) => {
                    this.TrackPoint = point;
                });
            }
        }

        #endregion // constructors


        #region properties

        public IDriveManager DriveManager
        {
            get;
            set;
        }

        public DeviceRepository Repository
        {
            get { return m_repository; }
        }

        public ICommand LoadCommand
        {
            get;
            private set;
        }

        public ICommand SearchCommand
        {
            get;
            private set;
        }

        public ICommand SaveCommand
        {
            get;
            private set;
        }

        #endregion // properties


        #region overriden methods

        protected override void CheckCommands()
        {
            base.CheckCommands();
        }

        #endregion // overriden methods


        #region internal methods

        // Load command
        private bool CanLoad(object data)
        {
            return true;
        }

        private void DoLoad(object data)
        {
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
                    Repository.Open(SelectedVehicle, folder, () => {
                        ResetTrackGroup(m_repository.GetTracks());
                        if (this.TrackGroup != null) {
                            this.SearchFrom = m_repository.StartTime.StripSeconds();
                            this.SearchTo = m_repository.EndTime.StripSeconds();
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
        private bool CanSearch()
        {
            return true;
        }

        private void DoSearch()
        {
            if (Tracks != null) {
                //Repository.ClearSelection(); // 기존 선택들을 굳이 해제시킬 필요는 없을것 같다.

                Tracks.Filter = (track) => {
                    DateTime d = ((Track)track).CreateDate.StripSeconds();
                    return d >= SearchFrom && d <= SearchTo;
                };
                ResetTrackGroup(null);
            }
        }

        // Save(sd card -> local storage) command
        private bool CanSave()
        {
            return true;
        }

        private void DoSave()
        {
            SaveViewModel model = new SaveViewModel(Repository, SearchFrom, SearchTo);
            DialogService.Run("저장", new SaveView(), model);
        }

        #endregion // internal methods
    }
}
