////////////////////////////////////////////////////////////////////////////////
// TrackCatalogCollection.cs
// 2012.03.26, created by sohong
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
using System.Collections.ObjectModel;
using System.IO;

namespace Viewer.Personal.Model {

    /// <summary>
    /// 스토리지에 저장된 catalog들을 읽어들이고,
    /// 검색하고,
    /// 변경하거나 추가하고,
    /// 저장한다.
    /// </summary>
    public class TrackCatalogCollection {

        #region fields

        private Vehicle m_vehicle;
        private string m_root;
        private ObservableCollection<TrackCatalog> m_catalogs;

        #endregion // fields


        #region constructors

        public TrackCatalogCollection() {
        }

        #endregion // constructors


        #region properties

        public Vehicle Vehicle {
            get { return m_vehicle; }
        }

        #endregion // properties


        #region methods

        public void Open(Vehicle vehicle, string rootFolder) {
            m_vehicle = vehicle;
            m_root = rootFolder;
            m_catalogs = new ObservableCollection<TrackCatalog>();

            Load();
        }

        public void Save(TrackCatalog catalog) {
        }

        #endregion // methods


        #region internal methods

        private void Load() {
            // vehicle's storage folder
            string folder = Path.Combine(m_root, Vehicle.VehicleId);
            if (Directory.Exists(folder)) {
                LoadTrackCatalog(folder);
            } else {
                CreateVehicleStorage(folder);
            }
        }

        private void LoadTrackCatalog(string folder) {
            m_catalogs.Clear();

            string[] files = Directory.GetFiles(folder, "*.xml");

            foreach (string file in files) {
                TrackCatalog cat = new TrackCatalog(Vehicle, 1, 1);
                cat.Load(file);
                m_catalogs.Add(cat);
            }
        }

        private void CreateVehicleStorage(string folder) {
            // vehicle's storage folder
            Directory.CreateDirectory(folder);
        }

        #endregion // internal methods
    }
}
