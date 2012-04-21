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
using System.ComponentModel;

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

            Vehicle.PropertyChanged += new PropertyChangedEventHandler((sender, e) => {
                CheckSubmit();
            });
            CheckSubmit();
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
            return Vehicle;
        }

        protected override bool CanSubmit() {
            return (Vehicle != null) && !string.IsNullOrWhiteSpace(Vehicle.Name);
        }

        protected override void DoSubmit() {
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
