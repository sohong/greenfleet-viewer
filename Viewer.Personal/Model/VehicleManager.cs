////////////////////////////////////////////////////////////////////////////////
// VehicleManager.cs
// 2012.03.15, created by sohong
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
using Viewer.Common.Xml;
using Viewer.Common.Util;

namespace Viewer.Personal.Model
{
    /// <summary>
    /// Vehicle을 읽고/쓰고/추가하고/수정하고/삭제한다.
    /// xml 파일에 저장하고 로드한다.
    /// </summary>
    public class VehicleManager
    {
        #region consts

        private const string VEHICLES_PATH = "vehicles.xml";
        private const string VEHICLE_ROOT = "Vehicles";
        private const string VEHICLE_ELEMENT = "Vehicle";

        #endregion // consts

        #region fields

        private ObservableCollection<Vehicle> m_vehicles;

        #endregion // fields


        #region constructor

        public VehicleManager()
        {
            m_vehicles = new ObservableCollection<Vehicle>();
        }

        #endregion // constructor


        #region properties

        public ObservableCollection<Vehicle> Vehicles
        {
            get { return m_vehicles; }
        }

        #endregion // properties


        #region methods

        public void Load()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, VEHICLES_PATH);
            if (!File.Exists(path)) {
                Save();
            }

            m_vehicles.Clear();
            new CollectionSerializer().Deserialize(path, VEHICLE_ELEMENT, typeof(Vehicle), m_vehicles);
        }

        public void Save()
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, VEHICLES_PATH);
            new CollectionSerializer().Serialize(m_vehicles, VEHICLE_ROOT, VEHICLE_ELEMENT, path);
        }

        public Vehicle Find(string vehicleName)
        {
            Vehicle vehicle = m_vehicles.FirstOrDefault((v) => {
                return string.Equals(v.Name, vehicleName, StringComparison.InvariantCultureIgnoreCase);
            });
            return vehicle;
        }

        public int IndexOf(Vehicle vehicle)
        {
            return m_vehicles.IndexOf(vehicle);
        }

        public void Add(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("vehicle");

            if (IndexOf(vehicle) < 0) {
                m_vehicles.Add(vehicle);
            }
        }

        public void Remove(Vehicle vehicle)
        {
            int index = IndexOf(vehicle);
            if (index >= 0) {
                RemoveAt(index);
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= 0 && index < m_vehicles.Count) {
                if (MessageUtil.Conform("차량 삭제", "선택하신 차량 정보를 삭제하시겠습니까?")) {
                    m_vehicles.RemoveAt(index);
                }
            }
        }

        #endregion // methods
    }
}
