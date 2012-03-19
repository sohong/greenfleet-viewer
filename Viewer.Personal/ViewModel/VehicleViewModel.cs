////////////////////////////////////////////////////////////////////////////////
// VehicleViewModel.cs
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
using Viewer.Common.ViewModel;
using Viewer.Personal.Model;
using System.Windows.Input;

namespace Viewer.Personal.ViewModel {
    
    /// <summary>
    /// View model for VehicleView.
    /// </summary>
    public class VehicleViewModel : DialogViewModel {

        #region fields

        private Vehicle m_source;
        
        #endregion // fields


        #region constructor

        public VehicleViewModel(Vehicle source) {
            m_source = source;
            if (source != null) {
                Vehicle = (Vehicle)source.Clone();
            } else {
                Vehicle = new Vehicle();
                string id = Guid.NewGuid().ToString();
                Vehicle.VehicleId = "v" + id.Substring(id.Length - 12, 12);
            }
        }

        #endregion // constructor


        #region properties

        public Vehicle Vehicle {
            get;
            private set;
        }

        #endregion // properties


        #region overriden methods

        protected override object GetSubmitData() {
            return (m_source != null) ? m_source : Vehicle;
        }

        protected override bool CanSubmit(object data) {
            return true;
        }

        protected override void DoSubmit(object data) {
            if (m_source == null) {
                // 추가
                PersonalDomain.Domain.Vehicles.Add(Vehicle);
            } else {
                // 수정
                Vehicle.AssignTo(m_source);
            }
        }

        #endregion // overriden methods
    }
}
