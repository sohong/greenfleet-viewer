////////////////////////////////////////////////////////////////////////////////
// LocalRepositoryViewModel.cs
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
using Viewer.Common.Event;
using Viewer.Common.Model;
using Viewer.Personal.Event;

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for LocalRepositoryView
    /// </summary>
    public class LocalRepositoryViewModel : RepositoryViewModel {

        #region fields
        #endregion // fields


        #region constructors

        public LocalRepositoryViewModel() {
            SearchFrom = DateTime.Today;
            SearchTo = DateTime.Today + TimeSpan.FromMinutes(23 * 60 + 59);
            SearchMode = SearchMode.Recent;
            AutoPlay = true;

            OpenCommand = new DelegateCommand<object>(DoOpen, CanOpen);
            ExportCommand = new DelegateCommand<object>(DoExport, CanExport);
            DeleteCommand = new DelegateCommand<object>(DoDelete, CanDelete);

            if (PersonalDomain.Domain.EventAggregator != null) {
                PersonalDomain.Domain.EventAggregator.GetEvent<LocalTrackActivatedEvent>().Subscribe((track) => {
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

        public LocalRepository Repository {
            get { return PersonalDomain.Domain.Repository; }
        }

        /// <summary>
        /// Search mode.
        /// </summary>
        public SearchMode SearchMode {
            get { return m_searchMode; }
            set {
                if (value != m_searchMode) {
                    m_searchMode = value;
                    RaisePropertyChanged(() => SearchMode);
                }
            }
        }
        private SearchMode m_searchMode;

        /// <summary>
        /// 지정한 구간의 트랙파일들을 읽어들인다.
        /// </summary>
        public ICommand OpenCommand {
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

        #endregion // properties


        #region overriden methods

        protected override void CheckCommands() {
            base.CheckCommands();
            ((DelegateCommand<object>)DeleteCommand).RaiseCanExecuteChanged();
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion // overriden methods


        #region internal methods

        // Search Command
        private bool CanOpen(object data) {
            return true;
        }

        private void DoOpen(object data) {
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
                dateFrom = new TrackFolderManager(Repository).GetRecentDay(SelectedVehicle);
                dateTo = dateFrom + TimeSpan.FromMinutes(23 * 60 + 59);
                break;

            case ViewModel.SearchMode.RecentTwo:
                dateFrom = new TrackFolderManager(Repository).GetRecentDay(SelectedVehicle) - TimeSpan.FromDays(1);;
                dateTo = dateFrom + TimeSpan.FromMinutes(47 * 60 + 59);
                break;

            case ViewModel.SearchMode.Range:
            default:
                break;
            }

            SearchFrom = dateFrom;
            SearchTo = dateTo;

            Repository.Find(SelectedVehicle, dateFrom, dateTo, () => {
                ResetTrackGroup(Repository.GetTracks());
            });
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

        #endregion // internal methods
    }
}
