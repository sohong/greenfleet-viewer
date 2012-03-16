////////////////////////////////////////////////////////////////////////////////
// Vehicle.cs
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
using Microsoft.Practices.Prism.ViewModel;

namespace Viewer.Personal.Model {
    
    /// <summary>
    /// 차량 정보.
    /// </summary>
    public class Vehicle : NotificationObject, ICloneable {

        #region constructors

        public Vehicle() {
        }

        #endregion // constructors


        #region properties

        /// <summary>
        /// Id
        /// </summary>
        public string VehicleId {
            get { return m_vehicleId; }
            set {
                if (value != m_vehicleId) {
                    m_vehicleId = value;
                    RaisePropertyChanged(() => VehicleId);
                }
            }
        }
        private string m_vehicleId = "-1";

        /// <summary>
        /// Name
        /// </summary>
        public string Name {
            get { return m_name; }
            set {
                if (value != m_name) {
                    m_name = value;
                    RaisePropertyChanged(() => Name);
                }
            }
        }
        private string m_name;

        /// <summary>
        /// 개요.
        /// </summary>
        public string Description {
            get { return m_description; }
            set {
                if (value != m_description) {
                    m_description = value;
                    RaisePropertyChanged(() => Description);
                }
            }
        }
        private string m_description;

        #endregion // properties


        #region IConneable 

        public object Clone() {
            Vehicle v = new Vehicle();
            AssignTo(v);
            return v;
        }

        #endregion // ICloneable


        #region methods

        public void AssignTo(Vehicle target) {
            target.VehicleId = this.VehicleId;
            target.Name = this.Name;
            target.Description = this.Description;
        }

        #endregion // methods
    }
}
