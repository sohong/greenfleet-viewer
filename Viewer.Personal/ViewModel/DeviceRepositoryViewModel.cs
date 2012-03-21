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
using Viewer.Personal.Command;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Viewer.Common.Model;
using Viewer.Common.Util;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for DeviceRepositoryView
    /// </summary>
    public class DeviceRepositoryViewModel : ViewModelBase {

        #region fields

        private ListCollectionView m_vehicles;
        private DeviceRepository m_repository;
        private ListCollectionView m_tracks;
        
        #endregion // fields


        #region constructors

        public DeviceRepositoryViewModel() {
            m_repository = new DeviceRepository();

            m_vehicles = new ListCollectionView(PersonalDomain.Domain.Vehicles);
            m_vehicles.CurrentChanged += new EventHandler(Vehicles_CurrentChanged);

            SearchFrom = DateTime.Today;
            SearchTo = DateTime.Today + TimeSpan.FromHours(23) + TimeSpan.FromMinutes(59);

            LoadCommand = new DelegateCommand<object>(DoLoad, CanLoad);
        }

        #endregion // constructors


        #region properties

        public ListCollectionView Vehicles {
            get { return m_vehicles; }
        }

        public DeviceRepository Repository {
            get { return m_repository; }
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
        /// 검색 조건 - 시작 시각(시/분)
        /// </summary>
        public DateTime SearchFrom {
            get { return m_searchFrom; }
            set {
                if (value != m_searchFrom) {
                    m_searchFrom = value;
                    RaisePropertyChanged(() => SearchFrom);
                }
            }
        }
        private DateTime m_searchFrom;

        /// <summary>
        /// 검색 조건 - 끝 시각(시/분)
        /// </summary>
        public DateTime SearchTo {
            get { return m_searchTo; }
            set {
                if (value != m_searchTo) {
                    m_searchTo = value;
                    RaisePropertyChanged(() => SearchTo);
                }
            }
        }
        private DateTime m_searchTo;

        /// <summary>
        /// 모드 가져오기
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

        public Commands Commands {
            get { return Commands.Instance; }
        }

        public ICommand LoadCommand {
            get;
            private set;
        }

        #endregion // properties


        #region internal methods

        private void Vehicles_CurrentChanged(object sender, EventArgs e) {
            SelectedVehicle = Vehicles.CurrentItem as Vehicle;
            CheckCommands();
        }

        private void CheckCommands() {
            //((DelegateCommand<object>)DeleteCommand).RaiseCanExecuteChanged();
            //CommandManager.InvalidateRequerySuggested();
        }

        // Load command
        private bool CanLoad(object data) {
            return true;
        }

        private void DoLoad(object data) {
            string folder = DialogUtil.SelectFolder("트랙 파일들이 저장된 위치를 선택하세요.", null);
            if (folder != null) {
                m_repository.Open(SelectedVehicle, folder);
                m_tracks = m_repository.GetTracks();
                this.TrackGroup = m_repository.LoadGroups(m_tracks);
            }
        }

        #endregion // internal methods
    }
}
