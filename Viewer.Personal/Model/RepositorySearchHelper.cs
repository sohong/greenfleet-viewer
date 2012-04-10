////////////////////////////////////////////////////////////////////////////////
// RepositorySearchHelper.cs
// 2012.04.10, created by sohong
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
using System.IO;
using Viewer.Common.Model;
using Viewer.Personal.ViewModel;

namespace Viewer.Personal.Model {

    /// <summary>
    /// Local repository에서 지정한 조건에 따라 트랙들을 가져온다.
    /// </summary>
    public class RepositorySearchHelper {

        #region fields

        private LocalRepository m_repository;
        private TrackFolderManager m_folderManager;

        #endregion // fields


        #region constructor

        public RepositorySearchHelper(LocalRepository repository) {
            m_repository = repository;
            m_folderManager = new TrackFolderManager(repository);
        }

        #endregion // constructor


        #region methods

        public IEnumerable<Track> Find(Vehicle vehicle, SearchMode mode, DateTime start, DateTime end) {
            IList<Track> tracks = new List<Track>();

            switch (mode) {
            case SearchMode.Recent:
                start = m_folderManager.FindRecentDay();
                break;
            case SearchMode.RecentTwo:
                break;
            case SearchMode.Today:
                break;
            case SearchMode.TwoDays:
                break;
            }

            Find(tracks, "", start, end);
            return tracks;
        }

        #endregion // methods


        #region internal methods

        private string GetRootFolder(Vehicle vehicle) {
            string folder = Path.Combine(m_repository.RootPath, vehicle.VehicleId);
            return folder;
        }

        private void Find(IList<Track> list, string root, DateTime start, DateTime end) {
        }

        #endregion // internal methods
    }
}
