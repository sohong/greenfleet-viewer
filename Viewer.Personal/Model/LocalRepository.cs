////////////////////////////////////////////////////////////////////////////////
// LocalRepository.cs
// 2012.03.07, created by sohong
//
// =============================================================================
// Copyright (C) 2012 PalmVision.
// All Rights Reserved.
////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Viewer.Common.Model;
using Viewer.Common.Util;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.IO;
using System.Text.RegularExpressions;
using Viewer.Common.Loader;

namespace Viewer.Personal.Model {
    
    /// <summary>
    /// Track 파일 저장소.
    /// Repository를 열면 catalog를 읽어 트랙 개체들을 생성한다.
    /// 각 트랙의 실제 데이터 파일과 영상 파일은 트랙을 재생할 때 연다. 
    /// </summary>
    public class LocalRepository : Repository {

        #region fields

        private string m_rootPath;
        private Dictionary<Vehicle, TrackCatalogCollection> m_catalogs;
        private TrackFolderManager m_folderManager;
        private TrackImportHelper m_importHelper;
        
        #endregion // fields


        #region constructors

        public LocalRepository() : base("Local") {
            m_catalogs = new Dictionary<Vehicle, TrackCatalogCollection>();
            m_folderManager = new TrackFolderManager(this);
            m_importHelper = new TrackImportHelper(this);
        }

        #endregion // constructors


        #region properties

        public string RootPath {
            get { return m_rootPath; }
        }

        #endregion // properties


        #region methods

        /// <summary>
        /// Repository를 연다.
        /// </summary>
        public void Open(string rootPath, IEnumerable<Vehicle> vehicles) {
            Logger.Info("Repository open...");

            m_rootPath = rootPath;
            if (!Directory.Exists(m_rootPath)) {
                Directory.CreateDirectory(m_rootPath);
            }

            LoadTrackCatalogs(vehicles);

            Logger.Info("Repository opened.");
        }

        /// <summary>
        /// 기존에 스토리지에 없는 차량이 추가되면 해당 폴더를 생성한다.
        /// </summary>
        public void AddVehicle(Vehicle vehicle) {
            if (!m_catalogs.ContainsKey(vehicle)) {
                TrackCatalogCollection cats = new TrackCatalogCollection();
                cats.Open(vehicle, RootPath);
                m_catalogs.Add(vehicle, cats);
            }
        }

        /// <summary>
        /// track file이 저장될 폴더명을 리턴한다.
        /// relative가 true이면 repository root 상대 경로로 리턴한다.
        /// 기존하지 않으면 생성한 후 리턴한다.
        /// </summary>
        public string GetFolder(Vehicle vehicle, string trackFile, bool relative) {
            return m_folderManager.GetFolder(vehicle, trackFile, relative);
        }

        public void Find(Vehicle vehicle, DateTime start, DateTime end, Action callback) {
            if (end >= start) {
                IList<string> trackFiles = new List<string>();
                string root = m_folderManager.GetRoot(vehicle);
                Find(trackFiles, root, start, end);

                if (trackFiles.Count > 0) {
                    TrackList.Load(trackFiles, trackFiles.Count, "트랙 검색", callback);
                }
            }
        }

        #endregion // methods


        #region internal methods

        private void LoadTrackCatalogs(IEnumerable<Vehicle> vehicles) {
            if (vehicles != null) {
                foreach (Vehicle v in vehicles) {
                    AddVehicle(v);
                }
            }
        }

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
