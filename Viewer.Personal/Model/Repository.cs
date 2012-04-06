////////////////////////////////////////////////////////////////////////////////
// Repository.cs
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

namespace Viewer.Personal.Model {
    
    /// <summary>
    /// Track 파일 저장소.
    /// </summary>
    public class Repository {

        #region static members

        public static readonly Regex TRACK_DATE_PATTERN = new Regex(@"\d{4}_\d{2}_\d{2}_\d{2}_\d{2}_\d{2}");

        public static bool ParseTrackFile(string fileName, ref DateTime date) {
            Match match = Repository.TRACK_DATE_PATTERN.Match(fileName);
            if (match.Success) {
                string[] arr = match.Value.Split('_');
                if (arr.Length >= 6) {
                    date = new DateTime(int.Parse(arr[0]), int.Parse(arr[1]), int.Parse(arr[2]),
                        int.Parse(arr[3]), int.Parse(arr[4]), int.Parse(arr[5]));
                    return true;
                }
            }
            return false;
        }

        #endregion // static members


        #region fields

        private string m_rootPath;
        private Dictionary<Vehicle, TrackCatalogCollection> m_catalogs;
        private ObservableCollection<Track> m_tracks;
        private TrackFolderManager m_folderManager;
        private TrackImportHelper m_importHelper;
        
        #endregion // fields


        #region constructors

        public Repository() {
            m_catalogs = new Dictionary<Vehicle, TrackCatalogCollection>();
            m_tracks = new ObservableCollection<Track>();
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
        /// Track 컬렉션 원본에 대한 뷰를 생성한다.
        /// </summary>
        public ListCollectionView GetTracks() {
            return new ListCollectionView(m_tracks);
        }

        /// <summary>
        /// track file이 저장될 폴더명을 리턴한다.
        /// relative가 true이면 repository root 상대 경로로 리턴한다.
        /// 기존하지 않으면 생성한 후 리턴한다.
        /// </summary>
        public string GetFolder(Vehicle vehicle, string trackFile, bool relative) {
            return m_folderManager.GetFolder(vehicle, trackFile, relative);
        }

        /// <summary>
        /// 외부 트랙파일들을 스토리지의 각 위치에 추가한다.
        /// files에는 확장자 없는 파일명들이 들어있다.
        /// </summary>
        public void ImportTrackFiles(Vehicle vehicle, IEnumerable<string> files, bool convert, bool overwrite) {
            Logger.Debug("Import track files...");
            m_importHelper.Import(vehicle, files, convert, overwrite);
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

        private Track Find(DateTime time) {
            return null;
        }

        private IEnumerable<Track> LoadTracks(DateTime fromTime, DateTime toTime, bool inclusive) {
            return null;
        }

        private Track LoadTrack(DateTime time) {
            return null;
        }

        private void SaveTracks(IEnumerable<Track> tracks) {
        }

        private void SaveTrack(Track track) {
        }

        #endregion // internal methods
    }
}
