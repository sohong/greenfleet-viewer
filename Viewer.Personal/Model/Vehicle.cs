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
    public class Vehicle : NotificationObject {

        #region constructor

        public Vehicle() {
        }

        #endregion // constructor


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
        private string m_vehicleId;

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
    }
}
