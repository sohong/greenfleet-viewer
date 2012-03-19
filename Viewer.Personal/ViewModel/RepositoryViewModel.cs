////////////////////////////////////////////////////////////////////////////////
// StorageViewModel.cs
// 2012.03.08, created by sohong
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
using System.Windows.Input;
using Viewer.Common.Service;
using Viewer.Personal.View;
using Microsoft.Practices.Prism.Commands;
using Viewer.Common.Util;
using Viewer.Personal.Command;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for RepositoryView
    /// </summary>
    public class RepositoryViewModel : ViewModelBase {

        #region fields

        private ListCollectionView m_tracks;
        private ListCollectionView m_vehicles;

        #endregion // fields


        #region constructors

        public RepositoryViewModel() {
            m_tracks = Repository.GetTracks();

            m_vehicles = new ListCollectionView(PersonalDomain.Domain.Vehicles);
            m_vehicles.CurrentChanged += new EventHandler(Vehicles_CurrentChanged);

            SearchCommand = new DelegateCommand<object>(DoSearch, CanSearch);
            ExportCommand = new DelegateCommand<object>(DoExport, CanExport);
            DeleteCommand = new DelegateCommand<object>(DoDelete, CanDelete);
            VehicleCommand = new DelegateCommand<object>(DoVechicle, CanVehicle);
            TestCommand = new DelegateCommand<object>(DoTest, CanTest);
        }

        #endregion // constructors


        #region properties

        public Repository Repository {
            get { return PersonalDomain.Domain.Repository; }
        }

        public ListCollectionView Tracks {
            get { return m_tracks; }
        }

        public ListCollectionView Vehicles {
            get { return m_vehicles; }
        }

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
        /// 검색 조건 - 시작 시간
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
        private DateTime m_searchFrom = DateTime.Today;

        /// <summary>
        /// 검색 조건 - 끝 시간
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
        private DateTime m_searchTo = DateTime.Today;

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

        /// <summary>
        /// Search command
        /// </summary>
        public ICommand SearchCommand {
            get;
            private set;
        }

        /// <summary>
        /// 선택한 트랙 정보들을 지정한 폴더로 이동 시킨다.
        /// </summary>
        public ICommand ExportCommand {
            get;
            private set;
        }

        /// <summary>
        /// 삭제 command
        /// </summary>
        public ICommand DeleteCommand {
            get;
            private set;
        }

        /// <summary>
        /// 차량 관리 command
        /// </summary>
        public ICommand VehicleCommand {
            get;
            private set;
        }

        /// <summary>
        /// Test command
        /// </summary>
        public ICommand TestCommand {
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
            ((DelegateCommand<object>)DeleteCommand).RaiseCanExecuteChanged();
            CommandManager.InvalidateRequerySuggested();
        }

        // Search Command
        private bool CanSearch(object data) {
            return true;
        }

        private void DoSearch(object data) {
        }

        // Export command
        private bool CanExport(object data) {
            return true;
        }

        private void DoExport(object data) {
        }

        // Delete command
        private bool CanDelete(object data) {
            return true;
        }

        private void DoDelete(object data) {
        }

        // Vehicle Command
        private bool CanVehicle(object data) {
            return true;
        }

        private void DoVechicle(object data) {
            DialogService.Run("차량 정보 관리", new VehicleListView(), new VehicleListViewModel());
        }

        // Test command
        private bool CanTest(object data) {
            return true;
        }

        private void DoTest(object data) {
        }

        #endregion // internal methods
    }
}
