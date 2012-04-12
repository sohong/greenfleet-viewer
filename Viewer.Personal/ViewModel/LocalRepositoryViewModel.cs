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

namespace Viewer.Personal.ViewModel {

    /// <summary>
    /// View model for LocalRepositoryView
    /// </summary>
    public class LocalRepositoryViewModel : RepositoryViewModel {

        #region fields

        private ListCollectionView m_tracks;

        #endregion // fields


        #region constructors

        public LocalRepositoryViewModel() {
            m_tracks = Repository.GetTracks();

            SearchCommand = new DelegateCommand<object>(DoSearch, CanSearch);
            ExportCommand = new DelegateCommand<object>(DoExport, CanExport);
            DeleteCommand = new DelegateCommand<object>(DoDelete, CanDelete);

            SearchFrom = DateTime.Today;
            SearchTo = DateTime.Today + TimeSpan.FromMinutes(23 * 60 + 59);
            SearchMode = SearchMode.Today;
            AutoPlay = true;

            if (PersonalDomain.Domain.EventAggregator != null) {
                PersonalDomain.Domain.EventAggregator.GetEvent<TrackActivatedEvent>().Subscribe((track) => {
                    track.IsChecked = true;
                    ActiveTrack = track;
                });
            }
        }

        #endregion // constructors


        #region properties

        public LocalRepository Repository {
            get { return PersonalDomain.Domain.Repository; }
        }

        public ListCollectionView Tracks {
            get { return m_tracks; }
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
        private bool CanSearch(object data) {
            return true;
        }

        private void DoSearch(object data) {
            IEnumerable<Track> tracks = new RepositorySearchHelper(Repository).Find(SelectedVehicle, SearchFrom, SearchTo);
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
