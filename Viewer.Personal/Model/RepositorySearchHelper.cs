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
using Viewer.Common.Loader;

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

        public IEnumerable<Track> Find(Vehicle vehicle, DateTime start, DateTime end) {
            if (end >= start) {
                IList<string> trackFiles = new List<string>();
                string root = m_folderManager.GetRoot(vehicle);
                Find(trackFiles, root, start, end);

                if (trackFiles.Count > 0) {
                    List<Track> tracks = new List<Track>();

                    return tracks;
                }
            }
            return null;
        }

        #endregion // methods


        #region internal methods

        private void Find(IList<string> list, string root, DateTime start, DateTime end) {
            while (start < end) {
                // 분단위로 판단할 수 있도록...
                start = start.AddSeconds(-start.Second);
                end = end.AddSeconds(60 - end.Second - 1);

                string folder = m_folderManager.DateTimeToFolder(root, start);
                if (Directory.Exists(folder)) {
                    string[] files = Directory.GetFiles(folder, "*.inc");
                    TrackType tt;
                    DateTime d;

                    foreach (string file in files) {
                        if (LocalTrackLoader.FileToDateTime(file, out d, out tt)) {
                            if (d >= start && d <= end) {
                                list.Add(file);
                            }
                        }
                    }
                }

                start += TimeSpan.FromDays(1);
            }
        }

        #endregion // internal methods
    }
}
